using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia.Threading;
using MsBox.Avalonia;
using Pfim;
using SkiaSharp;

namespace S5GUIEditor2;

internal class ImageCache
{
    internal required Settings? S { get; init; }

    private Dictionary<string, SKImage?> Cache = new();

    private SKImage? LoadImage(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;
        try
        {
            path = S!.ResolveS5Path(path);
            if (Path.GetExtension(path) != ".dds")
                return SKImage.FromEncodedData(path);

            // mostly from https://github.com/nickbabcock/Pfim/blob/master/src/Pfim.Skia/Program.cs
            SKColorType colorType;
            using var image = Pfimage.FromFile(path);
            var newData = image.Data;
            var newDataLen = image.DataLen;
            var stride = image.Stride;
            switch (image.Format)
            {
                case ImageFormat.Rgb8:
                    colorType = SKColorType.Gray8;
                    break;
                case ImageFormat.R5g6b5:
                    // color channels still need to be swapped
                    colorType = SKColorType.Rgb565;
                    break;
                case ImageFormat.Rgba16:
                    // color channels still need to be swapped
                    colorType = SKColorType.Argb4444;
                    break;
                case ImageFormat.Rgb24:
                    // Skia has no 24bit pixels, so we upscale to 32bit
                    var pixels = image.DataLen / 3;
                    newDataLen = pixels * 4;
                    newData = new byte[newDataLen];
                    for (int i = 0; i < pixels; i++)
                    {
                        newData[i * 4] = image.Data[i * 3];
                        newData[i * 4 + 1] = image.Data[i * 3 + 1];
                        newData[i * 4 + 2] = image.Data[i * 3 + 2];
                        newData[i * 4 + 3] = 255;
                    }

                    stride = image.Width * 4;
                    colorType = SKColorType.Bgra8888;
                    break;
                case ImageFormat.Rgba32:
                    colorType = SKColorType.Bgra8888;
                    break;
                default:
                    throw new ArgumentException($"Skia unable to interpret pfim format: {image.Format}");
            }

            var imageInfo = new SKImageInfo(image.Width, image.Height, colorType);
            var handle = GCHandle.Alloc(newData, GCHandleType.Pinned);
            var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(newData, 0);
            using var data = SKData.Create(ptr, newDataLen, (_, _) => handle.Free());
            return SKImage.FromPixels(imageInfo, data, stride);
        }
        catch (IOException)
        {
            return null;
        }
        catch (ArgumentException e)
        {
            Task.Run(async () =>
            {
                await Dispatcher.UIThread.InvokeAsync(async () =>
                {
                    await MessageBoxManager.GetMessageBoxStandard("exception", e.ToString()).ShowAsync();
                });
            });
            return null;
        }
    }

    internal SKImage? Get(string path)
    {
        if (Cache.TryGetValue(path, out SKImage? r))
            return r;
        SKImage? l = LoadImage(path);
        Cache[path] = l;
        return l;
    }
}
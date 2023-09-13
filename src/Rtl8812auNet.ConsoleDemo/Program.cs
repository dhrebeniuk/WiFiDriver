﻿using LibUsbDotNet.LibUsb;
using LibUsbDotNet;
using Rtl8812auNet.LibUsbDotNet;
using Rtl8812auNet.Rtl8812au;

namespace Rtl8812auNet.ConsoleDemo;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        using var context = new UsbContext();
        context.StartHandlingEvents();
        context.SetDebugLevel(LogLevel.Info);
        var devices = context.FindAll(SearchPredicate);

        var device = devices.First();
        var usb = new LibUsbRtlUsbDevice((UsbDevice)device);
        var rtlAdapter = new RtlUsbAdapter(usb);
        var rtlDevice = new Rtl8812aDevice(rtlAdapter);

        rtlDevice.Init();

        Console.ReadLine();
        Console.WriteLine("End");
    }

    private static bool SearchPredicate(IUsbDevice device)
    {
        return Rtl8812aDevices.Devices.Contains(new UsbDeviceVidPid(device.VendorId, device.ProductId));
    }
}
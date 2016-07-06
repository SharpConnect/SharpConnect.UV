//MIT,2016, EngineKit
using System.IO;
namespace System
{
    //for net20
    public static class RuntimeInformation
    {
        static bool isEvaluated;
        static OSPlatform evaluatedPlatform;
        public static bool IsOSPlatform(OSPlatform osPlatform)
        {
            if (!isEvaluated)
            {
                evaluatedPlatform = EvaluateRunningPlatform();
                isEvaluated = true;
            }
            return osPlatform == evaluatedPlatform;
        }
        static OSPlatform EvaluateRunningPlatform()
        {
            //credit: http://stackoverflow.com/questions/10138040/how-to-detect-properly-windows-linux-mac-operating-systems
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    // Well, there are chances MacOSX is reported as Unix instead of MacOSX.
                    // Instead of platform check, we'll do a feature checks (Mac specific root folders)
                    if (Directory.Exists("/Applications")
                        & Directory.Exists("/System")
                        & Directory.Exists("/Users")
                        & Directory.Exists("/Volumes"))
                        return OSPlatform.Mac;
                    else
                        return OSPlatform.Linux;

                case PlatformID.MacOSX:
                    return OSPlatform.Mac;

                default:
                    return OSPlatform.Windows;
            }
        }

    }
    public enum OSPlatform
    {
        Windows,
        Linux,
        Mac
    }

    public delegate void Action();
    public unsafe struct Span<T>
    {
        int len;
        IntPtr addr;
        bool fromFreeSpace;
        internal Span(IntPtr addr, int len)
        {
            this.len = len;
            this.addr = addr;
            fromFreeSpace = false;
        }

        internal Span(byte* addr, int len)
        {
            this.len = len;
            this.addr = new IntPtr((void*)addr);
            fromFreeSpace = false;
        }
        internal Span(void* addr, int len, bool fromFreeSpace)
        {
            this.len = len;
            this.addr = new IntPtr(addr);
            this.fromFreeSpace = fromFreeSpace;
        }
        public int Length
        {
            get { return len; }
        }
        public IntPtr UnsafePointer
        {
            get { return addr; }
        }
        internal bool FromFreeSpace
        {
            get { return fromFreeSpace; }
        }

    }

}

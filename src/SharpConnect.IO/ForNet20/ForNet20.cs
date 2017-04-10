//MIT, 2016-2017, EngineKit

using System.IO;
namespace System
{
    public delegate void Action();
    //for net20
    static class RuntimeInformation
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

     

}

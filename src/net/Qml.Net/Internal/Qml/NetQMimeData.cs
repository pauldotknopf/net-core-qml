using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Security;
using Qml.Net.Internal.Types;

namespace Qml.Net.Internal.Qml
{
    internal class NetQMimeData : BaseDisposable
    {
        public NetQMimeData(IntPtr handle, bool ownsHandle = true) 
            : base(handle, ownsHandle)
        {
        }
        protected override void DisposeUnmanaged(IntPtr ptr)
        {
            Interop.NetQMimeData.Destroy(ptr);
        }
        public bool HasColor {
            get {
                return Interop.NetQMimeData.HasColor(Handle);
            }
        }
        public bool HasHtml {
            get {
                return Interop.NetQMimeData.HasHtml(Handle);
            }
        }
        public bool HasImage {
            get {
                return Interop.NetQMimeData.HasImage(Handle);
            }
        }
        public bool HasText {
            get {
                return Interop.NetQMimeData.HasText(Handle);
            }
        }
        public bool HasUrls {
            get {
                return Interop.NetQMimeData.HasUrls(Handle);
            }
        }
    }
    internal class NetQMimeDataInterop
    {
        [NativeSymbol(Entrypoint = "net_qmimedata_destroy")]
        public DestroyDel Destroy { get; set; }

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DestroyDel(IntPtr qMimeData);

        [NativeSymbol(Entrypoint = "net_qmimedata_hasColor")]
        public HasColorDel HasColor {get; set; }

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool HasColorDel(IntPtr qMimeData);

        [NativeSymbol(Entrypoint = "net_qmimedata_hasHtml")]
        public HasHtmlDel HasHtml {get; set; }

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool HasHtmlDel(IntPtr qMimeData);

        [NativeSymbol(Entrypoint = "net_qmimedata_hasImage")]
        public HasImageDel HasImage {get; set; }

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool HasImageDel(IntPtr qMimeData);

        [NativeSymbol(Entrypoint = "net_qmimedata_hasText")]
        public HasTextDel HasText {get; set; }

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool HasTextDel(IntPtr qMimeData);

        [NativeSymbol(Entrypoint = "net_qmimedata_hasUrls")]
        public HasUrlsDel HasUrls {get; set; }

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool HasUrlsDel(IntPtr qMimeData);

    }
}
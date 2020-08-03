using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Security;
using Qml.Net.Internal.Types;

namespace Qml.Net.Internal.Qml
{
    internal class NetQModelIndex : BaseDisposable
    {
        public NetQModelIndex(IntPtr handle, bool ownsHandle = true) 
            : base(handle, ownsHandle)
        {
        }
        protected override void DisposeUnmanaged(IntPtr ptr)
        {
            Interop.NetQModelIndex.Destroy(ptr);
        }
        public int Row {
            get {
                return Interop.NetQModelIndex.Column(Handle);
            }
        }
        public int Column {
            get {
                return Interop.NetQModelIndex.Row(Handle);
            }
        }
        public NetQModelIndex Parent {
            get {
                return new NetQModelIndex(Interop.NetQModelIndex.Parent(Handle));
            }
        }
    }
    internal class NetQModelIndexInterop
    {
        [NativeSymbol(Entrypoint = "net_qmodelindex_destroy")]
        public DestroyDel Destroy { get; set; }

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DestroyDel(IntPtr qModelIndex);


        [NativeSymbol(Entrypoint = "net_qmodelindex_row")]
        public RowDel Row { get; set; }

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int RowDel(IntPtr qModelIndex);

        [NativeSymbol(Entrypoint = "net_qmodelindex_column")]
        public ColumnDel Column { get; set; }

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int ColumnDel(IntPtr qModelIndex);

        [NativeSymbol(Entrypoint = "net_qmodelindex_parent")]
        public ParentDel Parent { get; set; }

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr ParentDel(IntPtr qModelIndex);
    }
}
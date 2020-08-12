using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using Qml.Net.Internal;

namespace Qml.Net
{
    internal static class ReflectionExtensions {
        public static bool IsOverride(this MethodInfo m) {
            return m.GetBaseDefinition().DeclaringType != m.DeclaringType;
        }
        public static bool MemberIsOverride(this Type t, string name) {
            try {
                return ((MethodInfo)t.GetMember("Flags")[0]).IsOverride();
            } catch {
                return false;
            }
        }
    }
    public class QAbstractItemModel : BaseDisposable
    {
        public QAbstractItemModel()
            : base(Interop.NetAbstractItemModel.Create(), true)
        {
            var type = this.GetType();
            if (type.MemberIsOverride("Flags")) {
                flagsDel = (ptr) => {
                    return Flags(new QModelIndex(ptr, true));
                };
                flagsDelPtr = Marshal.GetFunctionPointerForDelegate(flagsDel);
            } else {
                flagsDel = Marshal.GetDelegateForFunctionPointer<NetAbstractItemModelInterop.FlagsDelegate>(Interop.NetAbstractItemModel.GetFlags(Handle));
            }
        }
        public virtual int Flags(QModelIndex index) {
            return flagsDel(Handle);
        }
        protected override void DisposeUnmanaged(IntPtr ptr)
        {
            Interop.NetAbstractItemModel.Destroy(ptr);
        }
        private NetAbstractItemModelInterop.FlagsDelegate flagsDel;
        private IntPtr flagsDelPtr;
        private NetAbstractItemModelInterop.DataDelegate dataDel;
        private IntPtr dataDelPtr;
        private NetAbstractItemModelInterop.HeaderDataDelegate headerDataDel;
        private IntPtr headerDataDelPtr;
        private NetAbstractItemModelInterop.RowCountDelegate rowCountDel;
        private IntPtr rowCountDelPtr;
        private NetAbstractItemModelInterop.ColumnCountDelegate columnCountDel;
        private IntPtr columnCountDelPtr;
        private NetAbstractItemModelInterop.IndexDelegate indexDel;
        private IntPtr indexDelPtr;
        private NetAbstractItemModelInterop.ParentDelegate parentDel;
        private IntPtr parentDelPtr;
    }
    internal class NetAbstractItemModelInterop
    {
        //
        // Pointer Types
        //
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int FlagsDelegate( IntPtr idx );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr DataDelegate( IntPtr idx, int role );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr HeaderDataDelegate( int section, int orientation, int role );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int RowCountDelegate( IntPtr parentIdx );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int ColumnCountDelegate( IntPtr parentIdx );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr IndexDelegate( int row, int col, IntPtr parent );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr ParentDelegate( IntPtr child );
        //
        // Instance Functions
        //
        [NativeSymbol(Entrypoint = "net_abstract_item_model_destroy")]
        public DestroyDel Destroy { get; set; }

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DestroyDel(IntPtr model);

        [NativeSymbol(Entrypoint = "net_abstract_item_model_create")]
        public CreateDel Create { get; set; }
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr CreateDel();

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr SetFuncDel(IntPtr instance, IntPtr func);
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetFuncDel(IntPtr instance);

        [NativeSymbol(Entrypoint = "net_abstract_item_model_set_flags")]
        public SetFuncDel SetFlags {get; set;}

        [NativeSymbol(Entrypoint = "net_abstract_item_model_get_flags")]
        public GetFuncDel GetFlags { get; set; }

        [NativeSymbol(Entrypoint = "net_abstract_item_model_set_data")]
        public SetFuncDel SetData {get; set;}

        [NativeSymbol(Entrypoint = "net_abstract_item_model_get_data")]
        public GetFuncDel GetData { get; set; }

        [NativeSymbol(Entrypoint = "net_abstract_item_model_set_header_data")]
        public SetFuncDel SetHeaderData {get; set;}

        [NativeSymbol(Entrypoint = "net_abstract_item_model_get_header_data")]
        public GetFuncDel GetHeaderData { get; set; }

        [NativeSymbol(Entrypoint = "net_abstract_item_model_set_row_count")]
        public SetFuncDel SetRowCount {get; set;}

        [NativeSymbol(Entrypoint = "net_abstract_item_model_get_row_count")]
        public GetFuncDel GetRowCount { get; set; }

        [NativeSymbol(Entrypoint = "net_abstract_item_model_set_column_count")]
        public SetFuncDel SetColumnCount {get; set;}

        [NativeSymbol(Entrypoint = "net_abstract_item_model_get_column_count")]
        public GetFuncDel GetColumnCount { get; set; }

        [NativeSymbol(Entrypoint = "net_abstract_item_model_set_index")]
        public SetFuncDel SetIndex {get; set;}

        [NativeSymbol(Entrypoint = "net_abstract_item_model_get_index")]
        public GetFuncDel GetIndex { get; set; }

        [NativeSymbol(Entrypoint = "net_abstract_item_model_set_parent")]
        public SetFuncDel SetParent {get; set;}

        [NativeSymbol(Entrypoint = "net_abstract_item_model_get_parent")]
        public GetFuncDel GetParent { get; set; }

    }
}
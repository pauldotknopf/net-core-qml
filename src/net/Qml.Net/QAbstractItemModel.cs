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
                Interop.NetAbstractItemModel.SetFlags(Handle, flagsDelPtr);
            } else {
                flagsDel = Marshal.GetDelegateForFunctionPointer<NetAbstractItemModelInterop.FlagsDelegate>(Interop.NetAbstractItemModel.GetFlags(Handle));
            }
            if (type.MemberIsOverride("Data")) {
                dataDel = (ptr, role) => {
                    var variant = new Internal.Qml.NetVariant();
                    var ret = Data(new QModelIndex(ptr, true), role);
                    Helpers.PackValue(ret, variant);
                    return variant.Handle;
                };
                dataDelPtr = Marshal.GetFunctionPointerForDelegate(dataDel);
                Interop.NetAbstractItemModel.SetData(Handle, dataDelPtr);
            } else {
                dataDel = Marshal.GetDelegateForFunctionPointer<NetAbstractItemModelInterop.DataDelegate>(Interop.NetAbstractItemModel.GetData(Handle));
            }
            if (type.MemberIsOverride("HeaderData")) {
                headerDataDel = (section, orientation, role) => {
                    var variant = new Internal.Qml.NetVariant();
                    var ret = HeaderData(section, orientation, role);
                    Helpers.PackValue(ret, variant);
                    return variant.Handle;
                };
                headerDataDelPtr = Marshal.GetFunctionPointerForDelegate(headerDataDel);
                Interop.NetAbstractItemModel.SetData(Handle, dataDelPtr);
            } else {
                headerDataDel = Marshal.GetDelegateForFunctionPointer<NetAbstractItemModelInterop.HeaderDataDelegate>(Interop.NetAbstractItemModel.GetHeaderData(Handle));
            }
            if (type.MemberIsOverride("RowCount")) {
                rowCountDel = (idx) => {
                    return RowCount(new QModelIndex(idx, true));
                };
                rowCountDelPtr = Marshal.GetFunctionPointerForDelegate(rowCountDel);
                Interop.NetAbstractItemModel.SetRowCount(Handle, rowCountDelPtr);
            } else {
                rowCountDel = Marshal.GetDelegateForFunctionPointer<NetAbstractItemModelInterop.RowCountDelegate>(Interop.NetAbstractItemModel.GetRowCount(Handle));
            }
            if (type.MemberIsOverride("ColumnCount")) {
                columnCountDel = (idx) => {
                    return RowCount(new QModelIndex(idx, true));
                };
                columnCountDelPtr = Marshal.GetFunctionPointerForDelegate(columnCountDel);
                Interop.NetAbstractItemModel.SetColumnCount(Handle, columnCountDelPtr);
            } else {
                columnCountDel = Marshal.GetDelegateForFunctionPointer<NetAbstractItemModelInterop.ColumnCountDelegate>(Interop.NetAbstractItemModel.GetColumnCount(Handle));
            }
            if (type.MemberIsOverride("Index")) {
                indexDel = (row, col, ptr) => {
                    return Index(row, col, new QModelIndex(ptr, true)).Handle;
                };
                indexDelPtr = Marshal.GetFunctionPointerForDelegate(indexDel);
                Interop.NetAbstractItemModel.SetIndex(Handle, indexDelPtr);
            } else {
                indexDel = Marshal.GetDelegateForFunctionPointer<NetAbstractItemModelInterop.IndexDelegate>(Interop.NetAbstractItemModel.GetColumnCount(Handle));
            }
            if (type.MemberIsOverride("Parent")) {
                parentDel = (child) => {
                    return Parent(new QModelIndex(child, true)).Handle;
                };
                parentDelPtr = Marshal.GetFunctionPointerForDelegate(parentDel);
                Interop.NetAbstractItemModel.SetParent(Handle, parentDelPtr);
            } else {
                parentDel = Marshal.GetDelegateForFunctionPointer<NetAbstractItemModelInterop.ParentDelegate>(Interop.NetAbstractItemModel.GetParent(Handle));
            }
        }
        public virtual int Flags(QModelIndex index) {
            return flagsDel(index.Handle);
        }
        public virtual object Data(QModelIndex index, int role) {
            var obj = dataDel(index.Handle, role);
            var variant = new Internal.Qml.NetVariant(obj, true);
            return variant.AsObject();
        }
        public virtual object HeaderData(int section, int orientation, int role) {
            var obj = headerDataDel(section, orientation, role);
            var variant = new Internal.Qml.NetVariant(obj, true);
            return variant.AsObject();
        }
        public virtual int RowCount(QModelIndex parent) {
            return rowCountDel(parent.Handle);
        }
        public virtual int ColumnCount(QModelIndex parent) {
            return columnCountDel(parent.Handle);
        }
        public virtual QModelIndex Index(int row, int column, QModelIndex parent) {
            return new QModelIndex(indexDel(row, column, parent.Handle), true);
        }
        public virtual QModelIndex Parent(QModelIndex child) {
            return new QModelIndex(parentDel(child.Handle), true);
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
#include <QtNetCoreQml/types/NetPropertyInfo.h>

NetPropertyInfo::NetPropertyInfo(QSharedPointer<NetTypeInfo> parentType,
        QString name,
        QSharedPointer<NetTypeInfo> returnType,
        bool canRead,
        bool canWrite) :
    _parentType(parentType),
    _name(name),
    _returnType(returnType),
    _canRead(canRead),
    _canWrite(canWrite)
{

}

QSharedPointer<NetTypeInfo> NetPropertyInfo::getParentType()
{
    return _parentType;
}

QString NetPropertyInfo::getPropertyName()
{
    return _name;
}

QSharedPointer<NetTypeInfo> NetPropertyInfo::getReturnType()
{
    return _returnType;
}

bool NetPropertyInfo::canRead()
{
    return _canRead;
}

bool NetPropertyInfo::canWrite()
{
    return _canWrite;
}

extern "C" {

Q_DECL_EXPORT NetPropertyInfoContainer* property_info_create(NetTypeInfoContainer* parentType,
                                               LPWSTR name,
                                               NetTypeInfoContainer* returnType,
                                               bool canRead,
                                               bool canWrite) {
    NetPropertyInfoContainer* result = new NetPropertyInfoContainer();
    NetPropertyInfo* instance = new NetPropertyInfo(parentType->netTypeInfo,
                                                    QString::fromUtf16((const char16_t*)name),
                                                    returnType->netTypeInfo,
                                                    canRead,
                                                    canWrite);
    result->property = QSharedPointer<NetPropertyInfo>(instance);
    return result;
}

Q_DECL_EXPORT void property_info_destroy(NetTypeInfoContainer* container) {
    delete container;
}

Q_DECL_EXPORT NetTypeInfoContainer* property_info_getParentType(NetPropertyInfoContainer* container) {
    NetTypeInfoContainer* result = new NetTypeInfoContainer();
    result->netTypeInfo = container->property->getParentType();
    return result;
}


Q_DECL_EXPORT LPWSTR property_info_getPropertyName(NetPropertyInfoContainer* container) {
    return (LPWSTR)container->property->getPropertyName().utf16();
}

Q_DECL_EXPORT NetTypeInfoContainer* property_info_getReturnType(NetPropertyInfoContainer* container) {
    NetTypeInfoContainer* result = new NetTypeInfoContainer();
    result->netTypeInfo = container->property->getReturnType();
    return result;
}

Q_DECL_EXPORT bool property_info_canRead(NetPropertyInfoContainer* container) {
    return container->property->canRead();
}

Q_DECL_EXPORT bool property_info_canWrite(NetPropertyInfoContainer* container) {
    return container->property->canWrite();
}

}

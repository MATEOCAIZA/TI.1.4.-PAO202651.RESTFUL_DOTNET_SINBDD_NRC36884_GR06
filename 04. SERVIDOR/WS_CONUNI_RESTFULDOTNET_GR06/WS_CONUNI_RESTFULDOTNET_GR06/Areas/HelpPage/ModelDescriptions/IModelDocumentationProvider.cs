using System;
using System.Reflection;

namespace WS_CONUNI_RESTFULDOTNET_GR06.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}
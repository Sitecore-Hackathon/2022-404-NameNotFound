using System;

namespace BasicCompany.Feature.BasicContent.Helpers
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class OrchestrationIgnoreAttribute : Attribute { }
}

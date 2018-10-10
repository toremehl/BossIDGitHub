using System;

#pragma warning disable 1591

namespace BossIDWS.Vendor.REST.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    ///     Describes a type model.
    /// </summary>
    public abstract class ModelDescription
    {
        public string Documentation { get; set; }

        public Type ModelType { get; set; }

        public string Name { get; set; }
    }
}
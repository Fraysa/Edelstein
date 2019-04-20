using System.Drawing;

namespace Edelstein.Provider.Templates.Field
{
    public class FieldReactorTemplate : ITemplate
    {
        public int ID { get; set; }

        public bool F { get; set; }
        public Point Position { get; set; }

        public FieldReactorTemplate(IDataProperty property)
        {
            ID = property.Resolve<int>("id") ?? -1;
            F = property.Resolve<bool>("f") ?? false;
            Position = new Point(
                property.Resolve<int>("x") ?? int.MinValue,
                property.Resolve<int>("y") ?? int.MinValue
            );
        }
    }
}
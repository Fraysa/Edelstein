using Edelstein.Network.Packets;
using Edelstein.Service.Game.Fields.Movements;

namespace Edelstein.Service.Game.Fields.Objects
{
    public abstract class AbstractFieldLife : AbstractFieldObj, IFieldLife
    {
        public byte MoveAction { get; set; }
        public short Foothold { get; set; }

        public MovePath Move(IPacket packet)
        {
            var path = new MovePath(packet);

            path.Apply(this);
            return path;
        }
    }
}
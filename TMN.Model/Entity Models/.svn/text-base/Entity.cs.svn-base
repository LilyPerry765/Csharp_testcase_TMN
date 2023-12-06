using System;
using System.Windows.Controls;
using System.Runtime.Serialization;

namespace TMN
{

    public abstract class Entity
    {
        public static Image GetImage(EntityTypes entity)
        {
            string path = null;
            Image img = new Image();
            img.Width = 16;
            img.Height = 16;
            switch (entity)
            {
                case EntityTypes.Center:
                    path = "../Images/Center.png";
                    break;
                case EntityTypes.Rack:
                    path = "../Images/Rack.png";
                    break;
                case EntityTypes.Shelf:
                    path = "../Images/Shelf.png";
                    break;
                case EntityTypes.Card:
                    path = "../Images/Card.png";
                    break;
                case EntityTypes.Link:
                    path = "../Images/link.png";
                    break;
            }
            img.SetImageSource(path);
            return img;
        }

        public string InferDisplayPath()
        {
            switch (EntityType)
            {
                case EntityTypes.Center:
                    return "DisplayName";
                case EntityTypes.Rack:
                    return "Name";
                case EntityTypes.Shelf:
                    return "Name";
                case EntityTypes.Card:
                    return "Name";
                case EntityTypes.Link:
                    return "Address";
                default:
                    throw new NotSupportedException();
            }
        }

        public static EntityTypes GetChildType(EntityTypes entityType)
        {
            if (entityType == EntityTypes.Link)
                throw new NotSupportedException("Link cannot have a child entity.");
            else if (entityType == EntityTypes.CardType)
                throw new NotSupportedException("CardType cannot have a child entity.");
            else
                return entityType + 1;
        }

        public EntityTypes GetChildType()
        {
            return GetChildType(EntityType);
        }

        public EntityTypes EntityType
        {
            get
            {
                EntityTypes type;
                if (!Enum.TryParse<EntityTypes>(this.GetType().Name, out type))
                    Enum.TryParse<EntityTypes>(this.GetType().Name.TrimEnd('s'), out type);
                return type;
            }
        }

        public Guid GetID()
        {
            return (Guid)this.GetType().GetProperty("ID", typeof(Guid)).GetValue(this, null);
        }

        public static bool AreEqual(Entity entity1, Entity entity2)
        {
            return entity1 != null && entity2 != null && entity1.EntityType == entity2.EntityType && entity1.GetID() == entity2.GetID();
        }

        /// <summary>
        /// Any additional data that needs to be sent with entity
        /// </summary>
        public virtual object Tag
        {
            get;
            set;
        } 

    }


}

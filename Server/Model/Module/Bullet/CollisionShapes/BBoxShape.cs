﻿using System;
using UnityEngine;
using System.Collections;
using BulletSharp;
using ETModel;

namespace BulletUnity {
    public class BBoxShape : BCollisionShape {

        protected Vector3 extents = Vector3.one;
        public Vector3 Extents
        {
            get { return extents; }
            set
            {
                if (collisionShapePtr != null && value != extents)
                {
                    Log.Error("Cannot change the extents after the bullet shape has been created. Extents is only the initial value " +
                                    "Use LocalScaling to change the shape of a bullet shape.");
                } else {
                    extents = value;
                }
            }
        }

        protected Vector3 m_localScaling = Vector3.one;
        public Vector3 LocalScaling
        {
            get { return m_localScaling; }
            set
            {
                m_localScaling = value;
                if (collisionShapePtr != null)
                {
                    ((BoxShape)collisionShapePtr).LocalScaling = value.ToBullet();
                }
            }
        }

        public override CollisionShape CopyCollisionShape()
        {
            BoxShape bs = new BoxShape(extents.ToBullet());
            bs.LocalScaling = m_localScaling.ToBullet();
            return bs;
        }

        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr == null) {
                collisionShapePtr = new BoxShape(extents.ToBullet());
                ((BoxShape)collisionShapePtr).LocalScaling = m_localScaling.ToBullet();
            }
            return collisionShapePtr;
        }
    }
}
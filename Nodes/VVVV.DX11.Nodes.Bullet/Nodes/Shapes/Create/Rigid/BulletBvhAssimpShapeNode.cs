﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VVVV.PluginInterfaces.V2;
using VVVV.Nodes.Bullet;
using VVVV.Utils.VMath;
using BulletSharp;
using VVVV.Bullet.DataTypes.Shapes.Rigid;
using AssimpNet;

namespace VVVV.Bullet.Nodes.Shapes.Create.Rigid
{
    [PluginInfo(Name = "Bvh", Category = "Bullet", Version="Assimp", Author = "vux")]
    public class BulletBvhAssimpShapeNode : AbstractBulletRigidShapeNode
    {
        [Input("Assimp Mesh",CheckIfChanged=true)]
        protected Pin<AssimpMesh> FInMesh;

        public override void Evaluate(int SpreadMax)
        {
            if ((this.FInMesh.IsChanged || this.BasePinsChanged) && this.FInMesh.PluginIO.IsConnected)
            {
                int spmax = ArrayMax.Max(FInMesh.SliceCount, this.BasePinsSpreadMax);

                for (int i = 0; i < spmax; i++)
                {
                    BvhShapeDefinition chull = new BvhShapeDefinition(this.FInMesh[i]);
                    chull.Mass = this.FMass[i];
                    this.SetBaseParams(chull, i);

                    this.FShapes[i] = chull;
                }
            }
        }
    }
}

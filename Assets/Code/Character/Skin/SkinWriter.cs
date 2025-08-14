using System;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class SkinWriter : Writer
    {
        public SkinModel Model;
        SkinModel model;

        public override void RequiredPix( in List <Type> a)
        {
            a.A <s_skin> ();
            a.A <d_dimension> ();
            a.A <s_element> ();
            
            if (Model.CompatibleIk)
            a.A <s_skin_foot_ik> ();

            if (Model.Hand != null && Model.Hand.Length >0 )
            a.A <d_hand> ();

            if (Model.SwordPlaces.Length > 0 || Model.BowPlaces.Length > 0)
            a.A <s_inv_0> ();
        }

        public override void OnWriteBlock()
        {
            // Instantiate the model
            model = Instantiate(Model).GetComponent<SkinModel>();

            new d_dimension.package ( model.h, model.r, model.m );
            new s_skin.package ( model.gameObject, new Vector2 (model.offsetRotationY, model.offsetPositionY) );

            if (model.Hand != null && model.Hand.Length >0 )
            new d_hand.package ( model.Hand );
            
            if (model.SwordPlaces.Length > 0 || model.BowPlaces.Length > 0)
            new s_inv_0.package (model.SwordPlaces, model.BowPlaces);
        }

        public override void AfterWrite(block b)
        {
            b.GetPix <s_element> ().SetElement ( model.Element.Write () );

            // destroy the model component
            Destroy (model);
        }
    }
}

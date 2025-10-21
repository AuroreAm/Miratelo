using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    [star(order.graphic_element)]
    public abstract class graphic_element : action {
        public static T instantiate_graphic_to_container <T> ( term prefab, RectTransform container ) where T : MaskableGraphic {
            T layer = res.go.instantiate ( prefab ).GetComponent <T> ();
            layer.transform.SetParent (container, false);
            layer.rectTransform.anchoredPosition = Vector2.zero;
            return layer;
        }
    }
}
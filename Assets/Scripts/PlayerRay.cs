using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class PlayerRay : MonoBehaviour
    {
        public Transform pointer;
        private Selectable currentlySelected;
        
        private void LateUpdate()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            //ray.origin = transform.position;
            //ray.direction = transform.forward;
            Debug.DrawRay(transform.position, transform.forward * 10f, Color.yellow);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                pointer.position = hit.point;

                Selectable selectable = hit.collider.gameObject.GetComponent<Selectable>();
                if (selectable)
                {
                    // Если объект был выбран, перекрашиваем его и обновляем текущий выбранный объект
                    if (selectable != currentlySelected)
                    {
                        if (currentlySelected != null)
                        {
                            currentlySelected.Deselect();
                        }

                        selectable.Select();
                        currentlySelected = selectable;
                    }
                }
                else
                {
                    // Если объект не выбран, перекрашиваем текущий выбранный объект в белый
                    if (currentlySelected != null)
                    {
                        currentlySelected.Deselect();
                        currentlySelected = null;
                    }
                }
            }
            else
            {
                // Если луч не попадает ни в какой объект, перекрашиваем текущий выбранный объект в белый
                if (currentlySelected != null)
                {
                    currentlySelected.Deselect();
                    currentlySelected = null;
                }
            }
        }

    }
}

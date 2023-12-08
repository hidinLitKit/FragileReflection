using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WeaponSystem;

namespace FragileReflection
{
    public class WeaponRay : MonoBehaviour
    {
        [Header("���������� ����� ���������")]
        [SerializeField] private bool showShots;
        [SerializeField] private GameObject shotMark;

        [Space]
        public Transform pointer;
        private Selectable currentlySelected;

        [SerializeField] LayerMask detectLayers;

        private void OnEnable()
        {
            GameEvents.onFire += TakeShot;
        }

        private void OnDisable()
        {
            GameEvents.onFire -= TakeShot;
        }

        private void LateUpdate()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward * 10f, Color.yellow);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                pointer.transform.gameObject.SetActive(true);
                pointer.position = hit.point;

                Selectable selectable = hit.collider.gameObject.GetComponent<Selectable>();
                if (selectable)
                {
                    // ���� ������ ��� ������, ������������� ��� � ��������� ������� ��������� ������
                    if (selectable != currentlySelected)
                    {
                        DisableSelect();

                        selectable.Select();
                        currentlySelected = selectable;
                        pointer.gameObject.SetActive(true);
                    }         
                }
                else
                {
                    // ���� ������ �� ������, ������������� ������� ��������� ������ � �����
                    DisableSelect();
                    pointer.gameObject.SetActive(false);
                }
            }
            else
            {
                pointer.transform.gameObject.SetActive(false);
                // ���� ��� �� �������� �� � ����� ������, ������������� ������� ��������� ������ � �����
                DisableSelect();
            }
        }

        private void DisableSelect()
        {
            if (currentlySelected != null)
            {
                currentlySelected.Deselect();
                currentlySelected = null;
            }
        }

        private void TakeShot()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward * 10f, Color.yellow);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, detectLayers))
            {
                if (hit.collider.gameObject.TryGetComponent(out IDamagable enemyHealth))
                {
                    enemyHealth.TakeDamage(WeaponManager.instance.currentWeapon.WeaponType.BodyDamage);
                    ShowShotPlace(hit);
                }
            }

        }

        private void ShowShotPlace(RaycastHit hit)
        {
            if(showShots)
            {
                Instantiate(shotMark, hit.point, Quaternion.identity);
            }
        }
    }
}

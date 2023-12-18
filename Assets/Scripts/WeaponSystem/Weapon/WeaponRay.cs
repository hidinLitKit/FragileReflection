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
        [SerializeField] private bool _showShots;
        [SerializeField] private GameObject _shotMark;

        [Header("������")]
        public Transform pointer;
        private Selectable currentlySelected;

        [Header("���� ������")]
        [SerializeField] LayerMask detectLayers;

        public void TakeShot()
        {
            Vector3 rayOrigin = transform.TransformPoint(Vector3.down * 0.7f);
            Ray ray = new Ray(rayOrigin, transform.forward);
            Debug.DrawRay(rayOrigin, transform.forward * 10f, Color.yellow);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, detectLayers))
            {
                if (hit.collider.gameObject.TryGetComponent(out IDamagable enemyHealth))
                {
                    enemyHealth.TakeDamage(WeaponManager.instance.currentWeapon.WeaponType.BodyDamage, WeaponManager.instance.currentWeapon.WeaponType.chance);
                    ShowShotPlace(hit);
                }
            }

        }

        private void LateUpdate()
        {
            Vector3 rayOrigin = transform.TransformPoint(Vector3.down * 0.7f);
            Ray ray = new Ray(rayOrigin, transform.forward);
            Debug.DrawRay(rayOrigin, transform.forward * 10f, Color.yellow);

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

        private void ShowShotPlace(RaycastHit hit)
        {
            if(_showShots)
            {
                Instantiate(_shotMark, hit.point, Quaternion.identity);
            }
        }
    }
}

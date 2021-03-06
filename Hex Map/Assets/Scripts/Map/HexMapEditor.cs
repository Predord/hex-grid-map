using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using HexGridProject.Core;

namespace HexGridProject.Map
{
    public class HexMapEditor : MonoBehaviour
    {
        public HexGrid hexGrid;

        private List<HexCell> selectedCells = new List<HexCell>();

        private bool alternativeAction;

        [SerializeField] private InputAction ClickCell;
        [SerializeField] private InputAction AlternativeActionAction;
        [SerializeField] private InputAction Cancel;

        private void Start()
        {
            ClickCell.performed += _ => OnPlayerTouchCell();
            AlternativeActionAction.performed += _ => AlternativeAction();
            Cancel.performed += _ => CancelSelectedCells();
        }

        private void OnEnable()
        {
            ClickCell.Enable();
            AlternativeActionAction.Enable();
            Cancel.Enable();
        }


        private void OnDisable()
        {
            ClickCell.Disable();
            AlternativeActionAction.Disable();
            Cancel.Disable();

            alternativeAction = false;
        }

        private void OnPlayerTouchCell()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                HexCell currentCell = GetCellUnderCursor();

                if (currentCell == null)
                    return;

                if (alternativeAction)
                {
                    if (selectedCells.Exists
                        (cell => cell.coordinates.X == currentCell.coordinates.X && cell.coordinates.Z == currentCell.coordinates.Z))
                    {
                        selectedCells.Remove(currentCell);
                        currentCell.DisableHighlight();

                        for(HexDirection direction = HexDirection.NorthEast; direction <= HexDirection.NorthWest; direction++)
                        {
                            HexCell neighbor = currentCell.GetNeighbor(direction);

                            if (neighbor != null && selectedCells.Exists
                                    (cell => cell.coordinates.X == neighbor.coordinates.X && cell.coordinates.Z == neighbor.coordinates.Z))
                            {
                                neighbor.EnableHighlight(Color.red, direction.Opposite());
                            }                          
                        }
                    }
                    else
                    {
                        selectedCells.Add(currentCell);

                        for (HexDirection direction = HexDirection.NorthEast; direction <= HexDirection.NorthWest; direction++)
                        {
                            HexCell neighbor = currentCell.GetNeighbor(direction);

                            if (neighbor != null && selectedCells.Exists
                                    (cell => cell.coordinates.X == neighbor.coordinates.X && cell.coordinates.Z == neighbor.coordinates.Z))
                            {
                                neighbor.DisableHighlight(direction.Opposite());
                            }
                            else
                            {
                                currentCell.EnableHighlight(Color.red, direction);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < selectedCells.Count; i++)
                    {
                        if (selectedCells[i] != null)
                            selectedCells[i].DisableHighlight();
                    }

                    selectedCells.Clear();

                    selectedCells.Add(currentCell);
                    currentCell.EnableHighlight(Color.red);
                }
            }
        }

        private HexCell GetCellUnderCursor()
        {
            Ray inputRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(inputRay, out RaycastHit hit))
            {
                return hexGrid.GetCell(hit.point);
            }

            return null;
        }

        private void AlternativeAction()
        {
            alternativeAction = !alternativeAction;
        }

        private void CancelSelectedCells()
        {
            for (int i = 0; i < selectedCells.Count; i++)
            {
                if (selectedCells[i] != null)
                    selectedCells[i].DisableHighlight();
            }

            selectedCells.Clear();
        }
    }
}

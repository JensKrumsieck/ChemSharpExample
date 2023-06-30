using System;
using ChemSharp.Molecules;
using UnityEngine;

public class MoleculeTest : MonoBehaviour
{
        public string path = @"Assets/CuHETMP.cif";
        void Start()
        {
            var mol = Molecule.FromFile(path);
            foreach (var atom in mol.Atoms)
            {
                var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                obj.transform.position = new Vector3(atom.Location.X, atom.Location.Y, atom.Location.Z);
                obj.transform.localScale =
                    new Vector3(atom.CovalentRadius, atom.CovalentRadius, atom.CovalentRadius) / 100f;
                var mat = new Material(Shader.Find("Standard"));
                ColorUtility.TryParseHtmlString(atom.Color, out var col);
                mat.color = col;
                obj.GetComponent<Renderer>().material = mat;
            }

            foreach (var bond in mol.Bonds)
            {
                var obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                var start = new Vector3(bond.Atom1.Location.X, bond.Atom1.Location.Y, bond.Atom1.Location.Z);
                var end =  new Vector3(bond.Atom2.Location.X, bond.Atom2.Location.Y, bond.Atom2.Location.Z);
                var loc = Vector3.Lerp(start, end, 0.5f);
                var lineVector = end - start;
                var axis = Vector3.Cross(Vector3.up, lineVector.normalized);
                var rad = Mathf.Acos(Vector3.Dot(Vector3.up, lineVector.normalized));
                var matrix = Quaternion.AxisAngle(axis, rad);
                obj.transform.localScale = new Vector3(0.05f, lineVector.magnitude / 2f, 0.05f);
                obj.transform.position = loc;
                obj.transform.rotation = matrix;
            }
        }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformer : MonoBehaviour
{
    public bool useDamage = true;
    public float damageRadius = 0.5f;
    private Vector3 localVector;
    public float maximumDamage = 0.5f;
	private float minimumCollisionForce = 5f;
    public float randomizeVertices = 1f;
    public float damageMultiplier = 1f;
    public MeshFilter[] deformableMeshFilters;
    private MeshDeformer.originalMeshVerts[] originalMeshData;
    public LayerMask damageFilter = -1;
    public bool repaired = true;
    private Quaternion rot = Quaternion.identity;
    private void DeformMesh(Mesh mesh, Vector3[] originalMesh, Collision collision, float cos, Transform meshTransform, Quaternion rot)
    {
        Vector3[] vertices = mesh.vertices;
        foreach (ContactPoint contact in collision.contacts)
        {
            Vector3 point = meshTransform.InverseTransformPoint(contact.point);
            for (int i = 0; i < vertices.Length; i++)
            {
                if ((point - vertices[i]).magnitude < this.damageRadius)
                {
                    vertices[i] += rot * (this.localVector * (this.damageRadius - (point - vertices[i]).magnitude) / this.damageRadius * cos + new Vector3(Mathf.Sin(vertices[i].y * 1000f), Mathf.Sin(vertices[i].z * 1000f), Mathf.Sin(vertices[i].x * 100f)).normalized * (this.randomizeVertices / 500f));
                    if (this.maximumDamage > 0f && (vertices[i] - originalMesh[i]).magnitude > this.maximumDamage)
                    {
                        vertices[i] = originalMesh[i] + (vertices[i] - originalMesh[i]).normalized * this.maximumDamage;
                    }
                }
            }
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length < 1 || collision.relativeVelocity.magnitude < this.minimumCollisionForce)
        {
            return;
        }

        if (this.useDamage && (1 << collision.gameObject.layer & this.damageFilter) != 0)
        {
            Vector3 colRelVel = collision.relativeVelocity;
            colRelVel *= 1f - Mathf.Abs(Vector3.Dot(base.transform.up, collision.contacts[0].normal));
            float cos = Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, colRelVel.normalized));
            if (colRelVel.magnitude * cos >= this.minimumCollisionForce)
            {
                this.repaired = false;
                this.localVector = base.transform.InverseTransformDirection(colRelVel) * (this.damageMultiplier / 50f);
                if (this.originalMeshData == null)
                {
                    this.LoadOriginalMeshData();
                }
                for (int i = 0; i < this.deformableMeshFilters.Length; i++)
                {
                    this.DeformMesh(this.deformableMeshFilters[i].mesh, this.originalMeshData[i].meshVerts, collision, cos, this.deformableMeshFilters[i].transform, this.rot);
                }

            }
        }
    }
    private void LoadOriginalMeshData()
    {
        this.originalMeshData = new MeshDeformer.originalMeshVerts[this.deformableMeshFilters.Length];
        for (int i = 0; i < this.deformableMeshFilters.Length; i++)
        {
            this.originalMeshData[i].meshVerts = this.deformableMeshFilters[i].mesh.vertices;
        }
    }
    private struct originalMeshVerts
    {
        // Token: 0x040009AE RID: 2478
        public Vector3[] meshVerts;
    }
}

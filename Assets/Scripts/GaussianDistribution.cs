using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussianDistribution : MonoBehaviour {

	public static double NextGaussianDouble(float std, float mean, float min, float max)
	{
		float U, u, v, S;

		do
		{
			u = 2.0f * Random.value - 1.0f;
			v = 2.0f * Random.value - 1.0f;
			S = u * u + v * v;
		}
		while (S >= 1.0f);

		double fac = Mathf.Sqrt(-2.0f * Mathf.Log((float) S, (float) (Mathf.Exp(1)) / S));
		double output = (u * fac)* std + mean;
		output = output <(double) min ? min : output;
		output = output >(double) max ? max : output;
		return output;
	}
}

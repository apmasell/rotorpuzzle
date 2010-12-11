using System;
using System.Collections.Generic;
namespace name.masella.rotorpuzzle {
	public class Configurations {
		private static bool[] BuildRotor(int[] offsets, int[] partitions, int rotorsize) {
			var result = new bool[rotorsize];

			for(int index = 0; index < offsets.Length; index++) {
				for(int count = 0; count < partitions[index]; count++) {
					var position = (offsets[index] + count * (rotorsize / partitions[index])) % rotorsize;
					if(result[position]) {
						return null;
					} else {
						result[position] = true;
					}
				}
			}

			return result;
		}

		public static IEnumerable<bool[]> Of(int[] partitions, int rotorsize) {
			var offsets = new int[partitions.Length];
			var index = 1;

			for(int i = 1; i < partitions.Length; i++) {
				offsets[i] = -1;
			}

			while(index > 0) {
				if(index >= partitions.Length) {
					var result = BuildRotor(offsets, partitions, rotorsize);
					if(result != null) {
						yield return result;
					}

					index--;
				} else {
					offsets[index]++;

					if(offsets[index] >= rotorsize / partitions[index]) {
						offsets[index] = -1;
						index--;
					} else {
						index++;
					}
				}
			}
		}
	}
}


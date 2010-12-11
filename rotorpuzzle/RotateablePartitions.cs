using System;
using System.Collections.Generic;
namespace name.masella.rotorpuzzle {
	class RotateablePartitions {
		public static IEnumerable<int[]> Of(int samples, int rotorsize) {
			foreach(var partitions in Partitions.Of(samples)) {
				if(IsRotateable(partitions, rotorsize)) {
					yield return partitions;
				}
			}
		}

		private static bool IsRotateable(int[] partitions, int rotorsize) {
			foreach(var partition in partitions) {
				if(partition == 1 || rotorsize % partition != 0) {
					return false;
				}
			}
			return true;
		}
	}
}
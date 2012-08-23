using System;
using System.Collections;
using System.Collections.Generic;
namespace name.masella.rotorpuzzle {
	public class Configurations {
		private static BitArray BuildRotor(int[] offsets, int[] partitions, int rotorsize) {
			var result = new BitArray(rotorsize);

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

		static bool Conflict(int partitionA, int offsetA, int partitionB, int offsetB, int rotorsize) {
			for(int a = 0; a < partitionA; a++) {
				for(int b = 0; b < partitionB; b++) {
					if((a * rotorsize / partitionA + offsetA) % rotorsize == (b * rotorsize / partitionB + offsetB) % rotorsize) {
						return true;
					}
				}
			}

			return false;
		}

		static int[] PopulateOffsets(int current, int rotorsize, int[] partitions, int[] offsets) {
			var possibilities = new List<int>();

			for(var offset = 1; offset < rotorsize / partitions[current]; offset++) {
				bool possible = true;

				for(var x = 0; x < current; x++) {
					if(Conflict(partitions[current], offset, partitions[x], offsets[x], rotorsize)) {
						possible = false;
						break;
					}
				}

				if(possible) {
					possibilities.Add(offset);
				}
			}

			if(possibilities.Count > 0) {
				return possibilities.ToArray();
			} else {
				return null;
			}
		}

		static long PackBitArray(BitArray array) {
			byte value = 0x00;

			for(byte x = 0; x < array.Count; x++) {
				value |= (byte)((array[x] == true) ? (0x01 << x) : 0x00);
			}

			return value;
		}

		public static IEnumerable<BitArray> Of(int[] partitions, int rotorsize) {
			var possibleoffsets = new int[partitions.Length - 1][];
			var offsets = new int[partitions.Length];
			var indicies = new int[partitions.Length];
			var closed = new Dictionary<long, bool>();
			var index = 1;

			while(index > 0) {
				if(index >= partitions.Length) {
					var result = BuildRotor(offsets, partitions, rotorsize);
					var value = PackBitArray(result);

					if(result != null && !closed.ContainsKey(value)) {
						closed.Add(value, true);
						yield return result;
					}

					index--;
				} else {
					if(possibleoffsets[index-1] == null) {
						indicies[index] = 0;
						possibleoffsets[index-1] = PopulateOffsets(index, rotorsize, partitions, offsets);

						if(possibleoffsets[index-1] == null) {
							index--;
						} else {
							offsets[index] = possibleoffsets[index-1][0];
							index++;
						}
					} else {
						indicies[index]++;

						if(indicies[index] >= possibleoffsets[index-1].Length) {
							possibleoffsets[index-1] = null;
							index--;
						} else {
							offsets[index] = possibleoffsets[index-1][indicies[index]];
							index++;
						}
					}
				}
			}
		}
	}
}


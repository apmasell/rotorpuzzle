using System;
using System.Collections.Generic;
namespace name.masella.rotorpuzzle {
	public class Partitions {
		private static int[] ArrayUpTo(int[] src, int length) {
			var dest = new int[length];
			Array.Copy(src, dest, length);
			return dest;
		}

		public static IEnumerable<int[]> Of(int n) {
			if (n < 2) {
				throw new ArgumentOutOfRangeException();
			}
			var a = new int [n+1];
			var k = 1;
			a[0] = 0;
			var y = n - 1;

			while(k != 0) {
				var x = a[k - 1] + 1;
				k --;

				while(2 * x <= y) {
					a[k] = x;
					y -= x;
					k ++;
				}

				var l = k + 1;

				while(x <= y) {
					a[k] = x;
					a[l] = y;
					yield return ArrayUpTo(a, k + 2);
					x ++;
					y --;
				}

				a[k] = x + y;
				y = x + y - 1;
				yield return ArrayUpTo(a, k + 1);
			}
		}
	}
}

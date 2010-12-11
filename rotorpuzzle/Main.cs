using System;
using System.Collections.Generic;
using System.Linq;
namespace name.masella.rotorpuzzle {
	class MainClass {
		public static void Main(string[] args) {
			for(int s = 2; s < 24; s++) {
				foreach(var x in RotateablePartitions.Of(s, 24)) {
					foreach(var rotor in Configurations.Of(x, 24)) {
						if(rotor != null) {
							Console.Write("{0} |", s);

							foreach(var v in rotor) {
								Console.Write(v ? 'X' : ' ');
							}

							Console.WriteLine('|');
						} else {
							Console.WriteLine("{0} is mysterious", s);
						}
					}
				}
			}
		}
	}
}


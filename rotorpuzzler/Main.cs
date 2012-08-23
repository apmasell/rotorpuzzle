using System;
using System.Collections.Generic;
namespace name.masella.rotorpuzzle {
	class MainClass {
		public static void Main(string[] args) {
			if(args.Length != 2) {
				Console.WriteLine("Rotor Puzzler usage: rotorpuzzler numsamples numrotorslots");
			} else {
				var samples = int.Parse(args[0]);
				var rotorslots = int.Parse(args[1]);

				foreach(var partitions in RotateablePartitions.Of(samples, rotorslots)) {
					foreach(var rotor in Configurations.Of(partitions, rotorslots)) {
						if(rotor != null) {
							Console.Write('|');

							foreach(bool v in rotor) {
								Console.Write(v ? 'X' : ' ');
							}

							Console.WriteLine('|');
						}
					}
				}
			}
		}
	}
}
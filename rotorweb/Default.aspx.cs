
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.UI;

namespace name.masella.rotorpuzzle {
	public partial class Default : System.Web.UI.Page {

		private const int ROTORDIAMETRE = 400;

		private bool[] BuildRotor(int samples, int rotorslots) {
			foreach(var partitions in RotateablePartitions.Of(samples, rotorslots)) {
				foreach(var rotor in Configurations.Of(partitions, rotorslots)) {
					return rotor;
				}
			}

			return null;
		}

		public virtual void configure(object sender, EventArgs args) {
			int numslots = 0;
			int numsamples = 0;

			if(int.TryParse(slots.Text, out numslots) && int.TryParse(samples.Text, out numsamples)) {
				if(numslots < 2) {
					status.Text = "Your rotor is too small. (That's what she said.)";
				} else if(numsamples < 2) {
					status.Text = "Cannot balance that number of samples.";
				} else if(numsamples > numslots) {
					status.Text = "Do multiple spins.";
				} else {
					var result = BuildRotor(numsamples, numslots);

					if(result == null) {
						status.Text = "None";
					} else {

						var bitmap = new Bitmap(ROTORDIAMETRE + 6, ROTORDIAMETRE + 6);
						var graphics = Graphics.FromImage(bitmap);
						graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
						var pen = new Pen(Color.Black, 1);
						var fill = new SolidBrush(Color.Blue);
						var blank = new SolidBrush(Color.White);
						float slotdiametre = (float)(((double)ROTORDIAMETRE) / (((double)numslots + 1) / Math.PI + 1.0));
						var font = new Font(FontFamily.GenericSansSerif, slotdiametre / 4);
						graphics.DrawEllipse(pen, 3, 3, ROTORDIAMETRE, ROTORDIAMETRE);

						for(int index = 0; index < result.Length; index++) {
							float x = (float)((ROTORDIAMETRE - slotdiametre) * (Math.Cos(index * 2 * Math.PI / numslots) + 1) / 2.0 + 3);
							float y = (float)((ROTORDIAMETRE - slotdiametre) * (Math.Sin(index * 2 * Math.PI / numslots) + 1) / 2.0 + 3);

							if(result[index]) {
								graphics.FillEllipse(fill, x, y, slotdiametre, slotdiametre);
							}

							graphics.DrawEllipse(pen, x, y, slotdiametre, slotdiametre);
							var text = (index + 1).ToString();
							var size = graphics.MeasureString(text, font);
							graphics.DrawString(text, font,
												result[index] ? blank : fill,
												x + slotdiametre / 2 - size.Width / 2,
												y + slotdiametre / 2 - size.Height / 2,
												StringFormat.GenericTypographic);
						}

						MemoryStream stream = new MemoryStream();
						bitmap.Save(stream, ImageFormat.Png);

						bitmap.Dispose();
						graphics.Dispose();

						rotor.Src = "data:image/png;base64," + Convert.ToBase64String(stream.ToArray());
					}
				}
			}
		}
	}
}


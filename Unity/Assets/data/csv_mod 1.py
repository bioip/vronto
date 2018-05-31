import csv 
import numpy as np 
import math 

def modify_xyz(file): 
	data = np.genfromtxt(file, dtype="i4, i4, i4, U100, U100", delimiter=',')
	num_rows = data.shape[0]
	for i in range(num_rows): 
		data[i][0] = int(data[i][0]) / 10
		data[i][1] = int(data[i][1]) / 10
		data[i][2] = int(data[i][2]) / 10
	np.savetxt(file, data, delimiter=',', fmt="%i %i %i %s %s", header="X, Y, Z, ID, Description", comments="")

def modify_csv_format(file):
	file_out = file[:file.find(".csv")] + "_mod.csv"
	with open(file, newline='') as csvIn, open(file_out, mode="w", newline='') as csvOut:
		reader = csv.reader(csvIn, delimiter='\t')
		for row in reader: 
			csvOut.write(','.join(row) + '\n')

def halp(file_in, file_out): 
	data = np.genfromtxt(file_in, skip_header=1, dtype="U100, f8, f8, f8, U100", delimiter=",")
	num_rows = data.shape[0] 

	orig_x = -32.13
	orig_y = 31.02
	y = orig_y 
	orig_z = -8.76
	distance = 1.5

	i = 0 
	while i <= 2250:
		k = 0 
		while k < 450:
			for j in range(30):
				if i + j + k >= 2448: 
					k = 450 
					i = 100000
					break
				data[i + j + k][1] = orig_x + j * distance * math.cos(math.pi / 4) 
				data[i + j + k][2] = y
				data[i + j + k][3] = orig_z + j * distance * math.cos(math.pi / 4) 
			k += 30
			y -= distance 
		i += 450
		y = orig_y 
	np.savetxt(file_out, data, delimiter=",", fmt="%s, %f, %f, %f, %s", header="ID, X, Y, Z, Description", comments="")


if __name__ == "__main__":
	halp("data_mod_experimental.csv", "data_mod2.csv")
	
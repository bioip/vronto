import csv 
import numpy as np 

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

if __name__ == "__main__":
	modify_csv_format("data.csv")
	# modify_csv_format("saved.csv")
	modify_xyz("data_mod.csv")
	
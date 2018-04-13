import csv 
import numpy as np 

def modify_xyz(file): 
	data = np.genfromtxt(file, dtype="f4, f4, f4, U100, U100", delimiter=',')
	num_rows = data.shape[0]
	
	for i in range(num_rows): 
		data[i][0] = data[i][0] / 20
		data[i][1] = data[i][1] / 20
		data[i][2] = data[i][2] / 20
	out = np.array([(x[3], x[0], x[1], x[2], x[4]) for x in data])
	np.savetxt(file, out, delimiter=',', fmt="%s", header="X, Y, Z, ID, Description", comments="")

def modify_csv_format(file):
	file_out = file[:file.find(".csv")] + "_mod.csv"
	with open(file, newline='') as csvIn, open(file_out, mode="w", newline='') as csvOut:
		reader = csv.reader(csvIn, delimiter='\t')
		for row in reader: 
			csvOut.write(','.join(row) + '\n')

if __name__ == "__main__":
	modify_csv_format("data.csv")
	modify_xyz("data_mod.csv")
	
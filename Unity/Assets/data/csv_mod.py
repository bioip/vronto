import csv 
import numpy as np 
import math 
import os

def modify_csv_format(file):

	with open(file, newline='') as csvIn, open('data_temp.csv', mode="w", newline='') as csvOut:

		reader = csv.reader(csvIn, delimiter='\t')
		
		# add header
		writer = csv.writer(csvOut, delimiter=',')
		writer.writerow(['X', 'Y', 'Z', 'ID', 'Label'])

		for row in reader: 
			csvOut.write(','.join(row) + '\n')
		
	file_out = file[:file.find(".csv")] + "_mod.csv"

	with open('data_temp.csv', mode='r', newline='') as infile, open(file_out, mode='w', newline='') as outfile:
		# re-order the columns
		fieldnames = ['ID', 'X', 'Y', 'Z', 'Label', 'Placed']
		writer = csv.DictWriter(outfile, fieldnames=fieldnames)
		writer.writeheader()
		for row in csv.DictReader(infile):
			row['Placed'] = 'False'
			writer.writerow(row)
	os.remove('data_temp.csv')


def modify_relationships_format(file):
	with open(file, newline='') as csvIn, open('data_temp.csv', mode="w", newline='') as csvOut:

		reader = csv.reader(csvIn, delimiter='\t')
		
		# add header
		writer = csv.writer(csvOut, delimiter=',')
		writer.writerow(['ID', 'Relationship', 'Target_ID'])

		for row in reader: 
			csvOut.write(','.join(row) + '\n')
		
	file_out = file[:file.find(".csv")] + "_mod.csv"

	with open('data_temp.csv', mode='r', newline='') as infile, open(file_out, mode='w', newline='') as outfile:
		# re-order the columns
		fieldnames = ['ID', 'Relationship', 'Target_ID']
		writer = csv.DictWriter(outfile, fieldnames=fieldnames)
		writer.writeheader()
		for row in csv.DictReader(infile):
			writer.writerow(row)
	os.remove('data_temp.csv')

		
		
		

def set_coordinates(file_in, file_out): 
	data = np.genfromtxt(file_in, skip_header=1, dtype="U100, f8, f8, f8, U100, U100", delimiter=",")

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
	np.savetxt(file_out, data, delimiter=",", fmt="%s, %f, %f, %f, %s, %s", header="ID, X, Y, Z, Description, Placed", comments="")
	os.remove(file_in)

if __name__ == "__main__":
	modify_csv_format("data.csv")
	modify_relationships_format("relationships.csv")
	set_coordinates("data_mod.csv", "data_final.csv")
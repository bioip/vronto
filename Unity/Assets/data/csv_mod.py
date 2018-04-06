import csv 

def modify_csv_format(file):
	file_out = file[:file.find(".csv")] + "_mod.csv"
	with open(file, newline='') as csvIn, open(file_out, mode="w", newline='') as csvOut:
		reader = csv.reader(csvIn, delimiter='\t')
		for row in reader: 
			csvOut.write(','.join(row) + '\n')

if __name__ == "__main__":
	modify_csv_format("data.csv")
	modify_csv_format("saved.csv")
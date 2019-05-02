# vronto

_VR coordinates for anatomy ontology classes._

This project explores the integration of virtual reality environments and multi-species anatomy ontologies (e.g. the [Hymenoptera Anatomy Ontology](http://portal.hymao.org)).  Our initial goal is simple, can we use a VR space to help generate 3D coordinates for anatomical terms in the ontology.  We anticipate that these coordinates will be useful in various ways:

1) By placing anatomical terms in a space corresponding to where they are roughly found on an organism we can use the cloud of points as a navigation system.  Users could highlight points on the head and return those terms that correspond to that space.

2) We could use 3D coordinates to _infer_ the relative topological relationships among terms, e.g. see the object properties in the [BSPO Biological Spatial Ontology](http://www.obofoundry.org/ontology/bspo.html).

3) With a reference coordinate system we can classify 2D images by simply annotating them with 2 or more points.  For example when images are databased they are typically assigned an annotation defining the "view" (e.g. anterior head; lateral thorax).  If instead the user annotates the image using annatomical points that have 3D coordinates then we can infer the view.

Early Demo: https://bioip.github.io/interfaces/moonshots/2018/08/28/vronto.html

Demo 2: https://drive.google.com/open?id=1a3PFtfv3hjlgBXJGRsC7_yODdTkwA8DA

Demo 3: https://drive.google.com/open?id=1Djw52z5vXc4A2M7-DFms1zSnsEjdMePr

# Getting Started

1) Download or clone this repository
2) Open the "Unity" folder in Unity 3d (version 2017.3.1f1 recommended to avoid version conflicts) to load the project.
3) To load your own model, first drag your model into the "Assets"-"Models" folder in the Unity Editor. Then drag your model to the "Hierachy", under the Gameobject "Model". The default position should be (X: 0, Y: 0, Z: 0). You can also tweak the scale and rotation of the model in the "Inspector".

![image](https://user-images.githubusercontent.com/36896710/53604222-7f0ab300-3b79-11e9-809e-2dd6243353e1.png)

4) To load your own CSV file and relationship file, first drag your file into the "Assets"-"data" folder in the Unity Editor. Then find the CSV gameobject in the Hierachy.

Your data should look like this:

![image](https://user-images.githubusercontent.com/36896710/54091682-e2060200-4350-11e9-9435-8fd497542208.png)

And your relationship file should look like this:

![image](https://user-images.githubusercontent.com/36896710/57106712-bcafc780-6cf3-11e9-81ec-79f148620f42.png)

Then run the csv_mod.py file with python. After that, you should get the data_final.csv file which looks like this, with coordinates assigned for each term.

![image](https://user-images.githubusercontent.com/36896710/57106616-7ce8e000-6cf3-11e9-9250-58154e5ee5ec.png)

And you should get the relationships_mod file which looks like this:

![image](https://user-images.githubusercontent.com/36896710/57106821-f84a9180-6cf3-11e9-8e0c-57bdd016a166.png)

Then use the final version of the CSV file - "data_final.csv". Drag the CSV file to the variable "File" in the CSV script component. Or if you have a saved_data file and want to reuse it to store your progress, then drag "saved_data.csv" file to the variable instead.

![image](https://user-images.githubusercontent.com/36896710/57106993-79a22400-6cf4-11e9-85b4-ea8e5a5fc851.png)

![image](https://user-images.githubusercontent.com/36896710/57106924-465f9500-6cf4-11e9-8c2d-fccf34026acf.png)

Similarly, for the relationship data file, drag the "relationships_mod.csv" to the variable "File" in the "CSV Relationship" script component.

![image](https://user-images.githubusercontent.com/36896710/57107018-8aeb3080-6cf4-11e9-8ff5-cf26b64086f6.png)


# Collaboration

The project was developed by UI CS students Josh Wu, Guanyunbo Yang, and industrial design student Lai Jiang under the supervision of Matt Yoder.

# Acknowledgements

This project was funded by NSF ABI 1356381. Ideas and opinions expressed here are those of the authors and not the NSF. 


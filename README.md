# vronto

_VR coordinates for anatomy ontology classes._

This project explores the integration of virtual reality environments and multi-species anatomy ontologies (e.g. the [Hymenoptera Anatomy Ontology](http://portal.hymao.org)).  Our initial goal is simple, can we use a VR space to help generate 3D coordinates for anatomical terms in the ontology.  We anticipate that these coordinates will be useful in various ways:

1) By placing anatomical terms in a space corresponding to where they are roughly found on an organism we can use the cloud of points as a navigation system.  Users could highlight points on the head and return those terms that correspond to that space.

2) We could use 3D coordinates to _infer_ the relative topological relationships among terms, e.g. see the object properties in the [BSPO Biological Spatial Ontology](http://www.obofoundry.org/ontology/bspo.html).

3) With a reference coordinate system we can classify 2D images by simply annotating them with 2 or more points.  For example when images are databased they are typically assigned an annotation defining the "view" (e.g. anterior head; lateral thorax).  If instead the user annotates the image using annatomical points that have 3D coordinates then we can infer the view.

# Getting Started

1) Download or clone this repository
2) Open the "Unity" folder in Unity (version 2017.3.1f1 recommended to avoid version conflicts) to load the project.

# Collaboration

The project was developed by UI CS students Josh Wu, Guanyunbo Yang, and industrial design student Lai Jiang under the supervision of Matt Yoder.

# Acknowledgements

This project was funded by NSF ABI 1356381. Ideas and opinions expressed here are those of the authors and not the NSF. 


import csv
import os

import bpy
from bpy import context

def initialize_model(file_name = '/Users/matt/src/github/hymao/blender/data/data.csv'):
    remove_meshes()
    with open(file_name, newline='') as csvfile:
        data = csv.reader(csvfile, delimiter='\t')
        i = 0
        for row in data:
            i = i + 1
            if i == 100:
                break;
            render_mesh(row)
    return

def save_model(file_name = '/Users/matt/src/github/hymao/blender/data/saved.csv'):
    saved_file = open(file_name, "w")
    select_meshes()
    for obj in bpy.context.selected_objects:
        if '--' in obj.name:
            object_id, name = obj.name.split('--')
            x = str(obj.location.x)
            y = str(obj.location.y)
            z = str(obj.location.z)
            saved_file.write("\t".join([x,y,z, object_id, name]) + "\n")
    saved_file.close

def render_mesh(row):
    my_sphere = bpy.ops.mesh.primitive_ico_sphere_add    

    label = row[3]
    object_id = row[4]

    my_sphere(location=(float(row[0]), float(row[1]), float(row[2])))
    bpy.context.object.name = '--'.join([label, object_id])
    True

def add_labels():
    select_meshes()
    for nr, obj in enumerate(bpy.context.selected_objects):
        True
    True

def remove_labels():
    True

def select_meshes():
    bpy.ops.object.select_by_type(extend=False, type='MESH')

def remove_meshes():
    select_meshes()
    if len(bpy.context.selected_objects) > 0:
        bpy.ops.object.delete(use_global=False)
    True




initialize_model()
save_model()



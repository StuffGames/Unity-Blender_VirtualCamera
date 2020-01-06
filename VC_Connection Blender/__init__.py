# This program is free software; you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation; either version 3 of the License, or
# (at your option) any later version.
#
# This program is distributed in the hope that it will be useful, but
# WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTIBILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
# General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with this program. If not, see <http://www.gnu.org/licenses/>.


bl_info = {
    "name" : "Unity VC Recieve",
    "author" : "Juan Sanchez Roa",
    "description" : "Recieving and interpreting the values created in the Unity Project",
    "blender" : (2, 80, 0),
    "version" : (0, 0, 1),
    "location" : "View3D",
    "warning" : "",
    "category" : "Object"
}

import bpy
from . camera_connection import Recieve_Camera_Connection
from . vc_addon_panel import VC_Addon_Panel

classes = (Recieve_Camera_Connection, VC_Addon_Panel)

def register():
    bpy.utils.register_class(Recieve_Camera_Connection)
    bpy.utils.register_class(VC_Addon_Panel)

def unregister():
    bpy.utils.unregister_class(Recieve_Camera_Connection)
    bpy.utils.unregister_class(VC_Addon_Panel)

if __name__ == "__main__":
    register()
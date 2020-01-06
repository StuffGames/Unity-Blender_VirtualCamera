import bpy

class VC_Addon_Panel(bpy.types.Panel):
    bl_idname = "OBJECT_PT_virtual_camera"
    bl_label = "Virtual Camera"
    bl_category = "VC Connection"
    bl_space_type = "VIEW_3D"
    bl_region_type = "UI"

    def draw(self, context):
        layout = self.layout
        row = layout.row()
        row.operator('object.connect_camera', text = "Connect Unity Values")
import bpy
from mathutils import Quaternion
from mathutils import Vector

class Recieve_Camera_Connection(bpy.types.Operator):
    bl_idname = "object.connect_camera"
    bl_label = "Link Values"
    bl_description = "Connect the Camera Values from Unity"
    #bl_options = {'REGISTER', 'UNDO'}

    def execute (self, context):
        file = open("QuaternionKeyFrameValues.txt","r")
        file.seek(0)

        camera = bpy.context.scene.objects['Camera']

        camera.rotation_mode = "QUATERNION"
        bpy.context.scene.frame_current = 0

        previousLocation = camera.location

        #bpy.context.scene.tool_settings.use_keyframe_insert_auto = True

        for line in file:
            values = line.split(",")
            bpy.context.scene.frame_current = int(values[0])
            camera.rotation_quaternion = Quaternion((float(values[1]),float(values[2]),float(values[3]),float(values[4])))
            bpy.ops.anim.keyframe_insert()
    
        #bpy.context.scene.tool_settings.use_keyframe_insert_auto = False
        file.close()

        bpy.ops.object.empty_add(type='PLAIN_AXES', location=camera.location)
        camera.parent = bpy.context.scene.objects["Empty"]
        camera.location = Vector((0,0,0))
        return {'FINISHED'}
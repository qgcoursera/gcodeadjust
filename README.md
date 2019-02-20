# gcodeadjust
Command line utility to adjust gcode files
------------------------------------------

Command Line Examples:

    Move 50 mm along the x-axis:   GCodeAdjust 50 0 0 <input.gcode >output.gcode
    
    Move 5.5 mm along the y-axis:   GCodeAdjust 0 5.5 0 <input.gcode >output.gcode
    
    Move -1.1 mm along the z-axis:   GCodeAdjust 0 0 -1.1 <input.gcode >output.gcode
    
    Move 50 mm along the x-axis, and 5.5 mm along the y-axis, and -1.1 mm along the z-axis:   GCodeAdjust 50 5.5 -1.1 <input.gcode >output.gcode
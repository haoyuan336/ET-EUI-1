const exportViewComponent = function(){


}
export {exportViewComponent};

let MemberTypeList = [
    "GImage",
    "GButton",
    "GLabel",
    "GProgressBar",
    "GSlider",
    "GComboBox",
    "GTextField",
    "GRichTextField",
    "GTextInput",
    "GLoader",
    "GList",
    "GGraph",
    "GGroup",
    "GMovieClip",
    "GTree",
]

function PublishViewCode(handler, codeWriter, classInfo, exportCodePath) {

    let className = classInfo.className + 'ViewComponent';

    let classFileName = exportCodePath + "/View/" + className + ".cs";


    codeWriter.reset();
    handler.SetupCodeFolder(exportCodePath + "/view", 'cs', classInfo.className);
    let members = classInfo.members;
    let membersCount = members.Count

    codeWriter.writeln("using FairyGUI;")
    codeWriter.writeln("namespace ET")

    codeWriter.startBlock()
    codeWriter.writeln("public class " + classInfo.className + "ViewComponent: Entity, IAwake, IUIScrollItem");

    codeWriter.startBlock();
    for (let j = 0; j < membersCount; j++) {
        let member = members.get_Item(j);
        if (member.type == "Transition") {
            continue
        }
        let isNormalType = CheckIsNormalType(member.type);
        if (isNormalType) {

            ConvertToNormalMember(codeWriter, member);
        } else {
            ConvertToComponentMember(codeWriter, member);
        }

    }
    codeWriter.endBlock();
    codeWriter.endBlock();

    codeWriter.save(classFileName)
}

function ConvertToNormalMember(codeWriter, member) {
    codeWriter.writeln('public %s  %s', member.type, member.name)
    codeWriter.startBlock()
    codeWriter.writeln("get")
    codeWriter.startBlock()
    codeWriter.writeln("if (this._%s == null)", member.name)
    codeWriter.startBlock()

    let asType = member.type.replace("G", "");

    codeWriter.writeln('this._%s = this.GetParent<FGUIBaseWindow>().GComponent.GetChild("%s").as%s;', member.name, member.name, asType)
    codeWriter.endBlock()
    codeWriter.writeln("return this._%s;", member.name)
    codeWriter.endBlock()
    codeWriter.endBlock()
}

function ConvertToComponentMember(codeWriter, member) {

    console.log("special type " + member.type)
    codeWriter.writeln('public  %sComponent %sComponent' , member.name, member.name)

    codeWriter.startBlock()
    codeWriter.writeln("get")
    codeWriter.startBlock()
    codeWriter.writeln("if (this._%sComponent== null)", member.name)
    codeWriter.startBlock()
    codeWriter.writeln('FGUIBaseWindow fguiBaseWindow = this.GetParent<FGUIBaseWindow>();')
    codeWriter.writeln();
    codeWriter.writeln('GComponent gComponent = fguiBaseWindow.GComponent;')
    codeWriter.writeln();
    codeWriter.writeln('FGUIBaseWindow childBaseWindow = this.AddChild<FGUIBaseWindow>();')
    codeWriter.writeln();
    codeWriter.writeln('childBaseWindow.GComponent = gComponent.GetChild("%s").asCom;', member.name)
    codeWriter.writeln();
    codeWriter.writeln('childBaseWindow.AddComponent<%sViewComponent>();', member.type)
    codeWriter.writeln();
    codeWriter.writeln('this._%sComponent = childBaseWindow.AddComponent<%sComponent>();', member.name, member.name)
    codeWriter.writeln();
    codeWriter.writeln();
    codeWriter.endBlock()
    codeWriter.writeln('return this._%sComponent;', member.name)
    codeWriter.endBlock()
    codeWriter.endBlock()
}
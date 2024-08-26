'use strict';
Object.defineProperty(exports, '__esModule', { value: true });
exports.onDestroy = exports.onPublish = void 0;
const GenCode_CSharp_1 = require('./GenCode_CSharp');
const csharp_1 = require("csharp");
const CodeWriter_1 = require("./CodeWriter");

let getMemberByName = true;
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
    "GComponent",
]

function PublishItemResourcesMap(handler, codeWriter, exportCodePath) {

    //到处资源对用的url

    let className = "ResourcesUrlMap"

    let path = "/Unity/Assets/Scripts/ModelView/Client/Plugins/EUI/"

    let classFileName = exportCodePath + path + className + '.cs';
    // let items = handler.items;
    // let itemCount = items.Count;
    handler.SetupCodeFolder(exportCodePath + path, 'cs', className)
    codeWriter.reset()
    codeWriter.writeln('using System;')
    codeWriter.writeln('using System.Collections.Generic;')
    codeWriter.writeln('using System.Reflection;')
    codeWriter.writeln('namespace ET')
    codeWriter.startBlock()
    codeWriter.writeln('public class %s', className)
    codeWriter.startBlock()
    codeWriter.writeln('private Dictionary<string, string> Maps = new Dictionary<string, string>();')
    codeWriter.writeln('private static %s Instance = null;', className)
    codeWriter.writeln('private static %s GetInstance()', className)
    codeWriter.startBlock()
    codeWriter.writeln('if (Instance == null)')
    codeWriter.startBlock()
    codeWriter.writeln('Instance = new %s();', className)
    codeWriter.writeln('Instance.InitData();')
    codeWriter.endBlock()
    codeWriter.writeln('return Instance;')
    codeWriter.endBlock()

    codeWriter.writeln('public static string Get(string itemName)')
    codeWriter.startBlock()
    codeWriter.writeln('return GetInstance().Maps[itemName];')
    codeWriter.endBlock()

    codeWriter.writeln('private void InitData()')
    codeWriter.startBlock()
    codeWriter.writeln('Type type = typeof (%s);', className)
    codeWriter.writeln('FieldInfo[] fieldInfos = type.GetFields();')
    codeWriter.writeln('foreach (var propertyInfo in fieldInfos)')
    codeWriter.startBlock()
    codeWriter.writeln('string name = propertyInfo.Name;')
    codeWriter.writeln('string value = Convert.ToString(propertyInfo.GetValue(this));')
    codeWriter.writeln('this.Maps.Add(name, value);')
    codeWriter.endBlock()
    codeWriter.endBlock()


    let project = handler.project;

    let packages = project.allPackages;

    for (let i = 0; i < packages.Count; i++) {

        let pkg = packages.get_Item(i);

        let items = pkg.items;

        codeWriter.writeln("//---------------" + pkg.name + "---------------");

        for (let j = 0; j < items.Count; j++) {

            let item = items.get_Item(j);

            let branch = item.branch;

            if (branch != "") {
                continue
            }

            let exported = item.exported;

            if (!exported) {
                continue
            }

            let itemName = item.name;

            let url = item.GetURL()

            // /^[^0-9]/
            if (itemName.match("^[A-Za-z]")) {
                codeWriter.writeln('public const string %s = "%s";', itemName, url)
            }

        }
    }



    // for (let i = 0; i < itemCount; i++) {

    //     let item = items.get_Item(i)
    //     let exported = item.exported;
    //     if (!exported) {
    //         continue
    //     }
    //     let itemName = item.name;
    //     let url = item.GetURL()

    //     // /^[^0-9]/
    //     if (itemName.match("^[A-Za-z]")) {
    //         codeWriter.writeln('public const string %s = "%s";', itemName, url)
    //     }
    // }
    codeWriter.endBlock()
    codeWriter.endBlock()
    codeWriter.save(classFileName)
}


function PublishFGUIComponentId(handler, codeWriter, classes, exportCodePath) {


    let className = "WindowID"
    // \Unity\Codes\ModelView\Demo\FGUI

    let path = "/Unity/Assets/Scripts/ModelView/Client/Plugins/EUI/"

    let classFileName = exportCodePath + path + className + '.cs';
    handler.SetupCodeFolder(exportCodePath + path, 'cs', className)

    codeWriter.reset()
    // using System;
    // using System.Collections.Generic;
    // using System.Reflection;




    codeWriter.writeln('using System;')
    codeWriter.writeln('using System.Collections.Generic;')
    codeWriter.writeln('using System.Linq;')


    codeWriter.writeln('namespace ET.Client')
    codeWriter.startBlock()
    codeWriter.writeln('public enum %s', className)
    codeWriter.startBlock()

    codeWriter.writeln('WindowID_Invaild = 0,')
    let project = handler.project;

    let packages = project.allPackages;

    let index = 1;

    let packageCount = packages.Count;

    let indexs = [];

    let packageNames = [];

    for (let i = 0; i < packageCount; i++) {

        let pkg = packages.get_Item(i);

        let items = pkg.items;

        codeWriter.writeln('//----------------------' + pkg.name + '---------------------');

        packageNames.push(pkg.name)

        indexs.push(index);

        for (let h = 0; h < items.Count; h++) {

            let item = items.get_Item(h);
            if (EndWith(item.name, "Layer") && item.type == "component") {

                codeWriter.writeln(item.name + ' = ' + index + ",")
                index++;

            }

        }

        indexs.push(index);




    }

    codeWriter.endBlock()

    console.log("pack age name s ", packageNames.length)

    codeWriter.writeln('public class ComponentPackageMap')

    codeWriter.startBlock();

    codeWriter.writeln('private static ComponentPackageMap Instance = null;');

    codeWriter.writeln('private Dictionary<WindowID, string> Maps = new Dictionary<WindowID, string>();');

    codeWriter.writeln('private static ComponentPackageMap GetInstance()');

    codeWriter.startBlock();

    codeWriter.writeln('if (Instance == null)')

    codeWriter.startBlock();

    codeWriter.writeln('Instance = new ComponentPackageMap();')

    codeWriter.writeln('Instance.InitData();')

    codeWriter.endBlock();

    codeWriter.writeln('return Instance;')

    codeWriter.endBlock();

    codeWriter.writeln('public static string Get(WindowID windowID)')

    codeWriter.startBlock();

    codeWriter.writeln('return GetInstance().Maps[windowID];');


    codeWriter.endBlock();

    codeWriter.writeln('private void InitData()');

    codeWriter.startBlock();


    for (let i = 0; i < packageNames.length; i++) {

        let name = packageNames[i];

        let startIndex = indexs[i * 2];

        let endIndex = indexs[i * 2 + 1];

        codeWriter.writeln('for (int j = %s; j < %s; j++)', startIndex, endIndex);

        codeWriter.startBlock();

        codeWriter.writeln("WindowID windowID = (WindowID)j;");

        codeWriter.writeln(" this.Maps.Add(windowID, \"%s\");", name);

        codeWriter.endBlock();

    }

    // 
    // {
    //     
    // }
    codeWriter.endBlock();

    codeWriter.writeln('public static string[] GetPackNames()')

    codeWriter.startBlock();

    codeWriter.writeln("return GetInstance().Maps.Values.ToArray();");

    codeWriter.endBlock();

    codeWriter.endBlock();

    codeWriter.endBlock()

    codeWriter.save(classFileName)



}

function PublishLogicCode(handler, codeWriter, classInfo, exportCodePath) {

    let className = classInfo.className + 'Component'
    let classFileName = exportCodePath + '/UIExportCode/Logic/' + className + '.cs';
    codeWriter.reset()
    codeWriter.writeln('')
    handler.SetupCodeFolder(exportCodePath + '/UIExportCode/Logic', 'cs', classInfo.className)

    codeWriter.writeln('using System.Collections.Generic;')
    codeWriter.writeln('using System.Linq;')
    codeWriter.writeln('namespace ET.Client')
    codeWriter.startBlock()

    codeWriter.writeln('[ComponentOf(typeof (UIBaseWindow))]')
    codeWriter.writeln('[ChildOf(typeof(UIBaseWindow))]')
    codeWriter.writeln('public class %s: Entity, IAwake, IUILogic', className)
    codeWriter.startBlock()
    codeWriter.writeln('public %sViewComponent View => this.GetParent<UIBaseWindow>().GetComponent<%sViewComponent>();', classInfo.className, classInfo.className)
    codeWriter.endBlock()
    codeWriter.endBlock()
    codeWriter.save(classFileName)

}



function PublishHotfixCode(handler, codeWriter, classInfo, exportCodePath) {

    let componentName = classInfo.className + 'Component'
    let className = classInfo.className + 'ComponentSystem'
    let classFileName = exportCodePath + '/UIExportCode/HotfixView/' + classInfo.className + '/' + className + '.cs';
    codeWriter.reset()
    codeWriter.writeln('')
    handler.SetupCodeFolder(exportCodePath + '/UIExportCode/HotfixView/' + classInfo.className + '/', 'cs', classInfo.className)

    codeWriter.writeln('using FairyGUI;')
    codeWriter.writeln('namespace ET.Client')
    codeWriter.startBlock()
    codeWriter.writeln('[FriendOf(typeof (%sComponent))]', classInfo.className)
    codeWriter.writeln('[FriendOf(typeof (UIBaseWindow))]')
    codeWriter.writeln('public static class %s', className)
    codeWriter.startBlock()
    codeWriter.writeln('public static void RegisterUIEvent(this %s self)', componentName)
    codeWriter.startBlock()
    codeWriter.endBlock()
    codeWriter.writeln('public static void ShowWindow(this %s self, Entity contextData = null)', componentName)
    codeWriter.startBlock()
    codeWriter.endBlock()
    codeWriter.writeln('public static void HideWindow(this %s self)', componentName)
    codeWriter.startBlock()
    codeWriter.endBlock()
    codeWriter.endBlock()
    codeWriter.endBlock()
    codeWriter.save(classFileName)
}
function ConvertComponentId(className) {

    return className;
}

function PublishHotfixEventCode(handler, codeWriter, classInfo, exportCodePath) {
    // LoginLayerEventHandler

    let componentName = classInfo.className + 'Component'
    let className = classInfo.className + 'EventHandler';
    let classFileName = exportCodePath + "/UIExportCode/HotfixView/" + classInfo.className + '/Event/' + className + ".cs";
    codeWriter.reset();
    handler.SetupCodeFolder(exportCodePath + "/UIExportCode/HotfixView/" + classInfo.className + "/Event/", 'cs', classInfo.className);
    codeWriter.writeln("using FairyGUI;")
    codeWriter.writeln('using UnityEngine;')
    codeWriter.writeln('namespace ET.Client')
    codeWriter.startBlock()
    codeWriter.writeln('[UIEvent(WindowID.%s)]', ConvertComponentId(classInfo.className))
    codeWriter.writeln('[FriendOf(typeof (UIBaseWindow))]')
    codeWriter.writeln('public class %s: IAUIEventHandler', className)
    codeWriter.startBlock()
    codeWriter.writeln('public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)')
    codeWriter.startBlock()
    codeWriter.writeln('uiBaseWindow.windowType = UIWindowType.Normal;')
    codeWriter.endBlock()
    codeWriter.writeln('public void OnInitComponent(UIBaseWindow uiBaseWindow)')
    codeWriter.startBlock()
    codeWriter.writeln('uiBaseWindow.AddComponent<%sViewComponent>();', classInfo.className)
    codeWriter.writeln('uiBaseWindow.AddComponent<%sComponent>();', classInfo.className)
    codeWriter.endBlock()
    codeWriter.writeln('public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)')
    codeWriter.startBlock()
    codeWriter.writeln('uiBaseWindow.GetComponent<%sComponent>().RegisterUIEvent();', classInfo.className)
    codeWriter.endBlock()
    codeWriter.writeln('public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)')
    codeWriter.startBlock()
    codeWriter.writeln('uiBaseWindow.GetComponent<%sComponent>().ShowWindow();', classInfo.className)
    codeWriter.endBlock()
    codeWriter.writeln('public void OnHideWindow(UIBaseWindow uiBaseWindow)')
    codeWriter.startBlock()
    codeWriter.writeln('uiBaseWindow.GetComponent<%sComponent>().HideWindow();', classInfo.className)
    codeWriter.endBlock()
    codeWriter.writeln('public void BeforeUnload(UIBaseWindow uiBaseWindow)')
    codeWriter.startBlock()
    codeWriter.endBlock()
    codeWriter.writeln('public void BindComponent(UIBaseWindow uiBaseWindow, GComponent gComponent)')
    codeWriter.startBlock()
    codeWriter.writeln('uiBaseWindow.GComponent = gComponent;')
    codeWriter.writeln('uiBaseWindow.GetComponent<%sViewComponent>().ClearBindCache();', classInfo.className, classInfo.className)
    codeWriter.endBlock()
    codeWriter.endBlock()
    codeWriter.endBlock()
    codeWriter.save(classFileName)

}

function PublishViewCode(handler, codeWriter, classInfo, exportCodePath) {

    let className = classInfo.className + 'ViewComponent';

    let path = "/Unity/Assets/Scripts/ModelView/Client/Demo/FGUI/View/"

    let classFileName = exportCodePath + path + className + ".cs";


    codeWriter.reset();
    handler.SetupCodeFolder(exportCodePath + path, 'cs', classInfo.className);
    let members = classInfo.members;
    let membersCount = members.Count

    codeWriter.writeln("using FairyGUI;")
    codeWriter.writeln("using ET.Client;")
    codeWriter.writeln("namespace ET")


    codeWriter.startBlock()
    codeWriter.writeln('[FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]')
    codeWriter.writeln("public class " + classInfo.className + "ViewComponent: Entity, IAwake");

    codeWriter.startBlock();
    for (let j = 0; j < membersCount; j++) {
        let member = members.get_Item(j);


        if (member.name.length <= 3) {
            continue;
        }

        let item = member.res;

        if (item != null) {

            console.log("item", item)

            let exported = item.exported;
            if (!exported) {
                continue
            }
        }

        // let item = member.res
        // if (!item.exported){
        // continue
        // }
        console.log("memner type " + member.type + ',' + member.name)
        // let res = member.res;

        if (member.name.match(".*(?:Button)$")) {

            ConvertToButtonMember(codeWriter, member);
            continue
        }
        if (member.name.match(".*(?:Comp)$")) {

            ConvertToCompMember(codeWriter, member);
            continue
        }

        if (member.name.match(".*(?:ComboBox)$")) {

            ConvertToComboBoxMember(codeWriter, member);
            continue
        }

        // return name.match(".*(?:Layer|Cell|BagList)$", str);
        if (member.name.match(".*(?:Progress)$")) {
            ConvertToProgressMember(codeWriter, member);
            continue
        }


        // console.log("res", res)

        if (member.type == "Transition") {
            ConvertToTransitionMember(codeWriter, member)
            continue
        }
        if (member.type == "Controller") {

            ConvertToControllMember(codeWriter, member)
            continue
        }
        let isNormalType = CheckIsNormalType(member.type);
        if (isNormalType) {

            ConvertToNormalMember(codeWriter, member);
        } else {
            ConvertToComponentMember(codeWriter, member);
        }
    }


    for (let j = 0; j < membersCount; j++) {
        let member = members.get_Item(j);


        if (member.name.length <= 3) {
            continue;
        }

        if (member.name.match(".*(?:Progress)$")) {
            codeWriter.writeln('private GProgressBar _%s = null;', member.name)
            continue
        }

        if (member.name.match(".*(?:Button)$")) {

            codeWriter.writeln('private GButton _%s = null;', member.name)
            continue
        }
        if (member.name.match(".*(?:Comp)$")) {

            codeWriter.writeln('private GComponent _%s = null;', member.name)
            continue
        }
        if (member.name.match(".*(?:ComboBox)$")) {

            codeWriter.writeln('private GComboBox _%s = null;', member.name)
            continue
        }
        if (member.type == "Controller") {
            codeWriter.writeln('private %s _%s = null;', member.type, member.name)
            continue
        }

        if (member.type == "Transition") {
            codeWriter.writeln('private %s _%s = null;', member.type, member.name)
            continue
        }
        let isNormalType = CheckIsNormalType(member.type)
        if (isNormalType) {

            codeWriter.writeln('private %s _%s = null;', member.type, member.name)
        } else {

            codeWriter.writeln('private %sComponent _%sComponent = null;', member.type, member.name)
        }
    }

    codeWriter.writeln('public void ClearBindCache()')
    codeWriter.startBlock()
    for (let j = 0; j < membersCount; j++) {
        let member = members.get_Item(j);

        // let varMemberName = member.varName;

        // console.log("var member name ", varMemberName)

        console.log("type ", member.type)

        if (member.name.length <= 3) {
            continue;
        }


        if (member.name.match(".*(?:Progress)$")) {
            codeWriter.writeln('this._%s = null;', member.name)
            continue
        }

        if (member.name.match(".*(?:Button)$")) {

            codeWriter.writeln('this._%s = null;', member.name)
            continue
        }
        if (member.name.match(".*(?:Comp)$")) {

            codeWriter.writeln('this._%s = null;', member.name)
            continue
        }
        if (member.name.match(".*(?:ComboBox)$")) {

            codeWriter.writeln('this._%s = null;', member.name)
            continue
        }

        if (member.type == "Transition") {
            codeWriter.writeln('this._%s = null;', member.name)
            continue;
        }
        if (member.type == "Controller") {
            codeWriter.writeln('this._%s = null;', member.name)
            continue
        }

        let isNormalType = CheckIsNormalType(member.type)
        if (isNormalType) {

            codeWriter.writeln('this._%s = null;', member.name)
        } else {
            codeWriter.writeln('this._%sComponent = null;', member.name)

        }

    }

    codeWriter.endBlock()


    codeWriter.endBlock();
    codeWriter.endBlock();

    codeWriter.save(classFileName)
}

function ConvertToTransitionMember(codeWriter, member) {
    codeWriter.writeln('public %s  %s', member.type, member.name)
    codeWriter.startBlock()
    codeWriter.writeln("get")
    codeWriter.startBlock()
    codeWriter.writeln("if (this._%s == null)", member.name)
    codeWriter.startBlock()


    codeWriter.writeln('this._%s = this.GetParent<UIBaseWindow>().GComponent.GetTransition("%s");', member.name, member.name)
    codeWriter.endBlock()
    codeWriter.writeln("return this._%s;", member.name)
    codeWriter.endBlock()
    codeWriter.endBlock()

}

function ConvertToControllMember(codeWriter, member) {
    codeWriter.writeln('public %s  %s', member.type, member.name)
    codeWriter.startBlock()
    codeWriter.writeln("get")
    codeWriter.startBlock()
    codeWriter.writeln("if (this._%s == null)", member.name)
    codeWriter.startBlock()


    codeWriter.writeln('this._%s = this.GetParent<UIBaseWindow>().GComponent.GetController("%s");', member.name, member.name)
    codeWriter.endBlock()
    codeWriter.writeln("return this._%s;", member.name)
    codeWriter.endBlock()
    codeWriter.endBlock()

}

function ConvertToButtonMember(codeWriter, member) {
    codeWriter.writeln('public GButton  %s', member.name)
    codeWriter.startBlock()
    codeWriter.writeln("get")
    codeWriter.startBlock()
    codeWriter.writeln("if (this._%s == null)", member.name)
    codeWriter.startBlock()

    let asType = member.type.replace("G", "");

    codeWriter.writeln('this._%s = this.GetParent<UIBaseWindow>().GComponent.GetChild("%s").asButton;', member.name, member.name)
    codeWriter.endBlock()
    codeWriter.writeln("return this._%s;", member.name)
    codeWriter.endBlock()
    codeWriter.endBlock()
}
function ConvertToCompMember(codeWriter, member) {
    codeWriter.writeln('public GComponent  %s', member.name)
    codeWriter.startBlock()
    codeWriter.writeln("get")
    codeWriter.startBlock()
    codeWriter.writeln("if (this._%s == null)", member.name)
    codeWriter.startBlock()

    let asType = member.type.replace("G", "");

    codeWriter.writeln('this._%s = this.GetParent<UIBaseWindow>().GComponent.GetChild("%s").asCom;', member.name, member.name)
    codeWriter.endBlock()
    codeWriter.writeln("return this._%s;", member.name)
    codeWriter.endBlock()
    codeWriter.endBlock()
}
function ConvertToComboBoxMember(codeWriter, member) {
    codeWriter.writeln('public GComboBox  %s', member.name)
    codeWriter.startBlock()
    codeWriter.writeln("get")
    codeWriter.startBlock()
    codeWriter.writeln("if (this._%s == null)", member.name)
    codeWriter.startBlock()

    let asType = member.type.replace("G", "");

    codeWriter.writeln('this._%s = this.GetParent<UIBaseWindow>().GComponent.GetChild("%s").asComboBox;', member.name, member.name)
    codeWriter.endBlock()
    codeWriter.writeln("return this._%s;", member.name)
    codeWriter.endBlock()
    codeWriter.endBlock()
}
function ConvertToProgressMember(codeWriter, member) {
    codeWriter.writeln('public GProgressBar  %s', member.name)
    codeWriter.startBlock()
    codeWriter.writeln("get")
    codeWriter.startBlock()
    codeWriter.writeln("if (this._%s == null)", member.name)
    codeWriter.startBlock()

    let asType = member.type.replace("G", "");

    codeWriter.writeln('this._%s = this.GetParent<UIBaseWindow>().GComponent.GetChild("%s").asProgress;', member.name, member.name)
    codeWriter.endBlock()
    codeWriter.writeln("return this._%s;", member.name)
    codeWriter.endBlock()
    codeWriter.endBlock()
}

function ConvertToNormalMember(codeWriter, member) {
    codeWriter.writeln('public %s  %s', member.type, member.name)
    codeWriter.startBlock()
    codeWriter.writeln("get")
    codeWriter.startBlock()
    codeWriter.writeln("if (this._%s == null)", member.name)
    codeWriter.startBlock()

    let asType = member.type.replace("G", "");

    codeWriter.writeln('this._%s = this.GetParent<UIBaseWindow>().GComponent.GetChild("%s").as%s;', member.name, member.name, asType)
    codeWriter.endBlock()
    codeWriter.writeln("return this._%s;", member.name)
    codeWriter.endBlock()
    codeWriter.endBlock()
}



function ConvertToComponentMember(codeWriter, member) {

    console.log("special type " + member.type)
    codeWriter.writeln('public  %sComponent %sComponent', member.type, member.name)

    codeWriter.startBlock()
    codeWriter.writeln("get")
    codeWriter.startBlock()
    codeWriter.writeln("if (this._%sComponent== null)", member.name)
    codeWriter.startBlock()
    codeWriter.writeln('UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();')
    codeWriter.writeln();
    codeWriter.writeln('GComponent gComponent = fguiBaseWindow.GComponent;')
    codeWriter.writeln();
    codeWriter.writeln('UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();')
    codeWriter.writeln();
    codeWriter.writeln('childBaseWindow.WindowID = WindowID.%s;', member.type)
    codeWriter.writeln();
    codeWriter.writeln('childBaseWindow.GComponent = gComponent.GetChild("%s").asCom;', member.name)
    // codeWriter.writeln('childBaseWindow.GComponent.fairyBatching = true;');
    codeWriter.writeln();
    codeWriter.writeln('childBaseWindow.AddComponent<%sViewComponent>();', member.type)
    codeWriter.writeln();
    codeWriter.writeln('this._%sComponent = childBaseWindow.AddComponent<%sComponent>();', member.name, member.type)
    codeWriter.writeln();
    codeWriter.writeln();
    codeWriter.endBlock()
    codeWriter.writeln('return this._%sComponent;', member.name)
    codeWriter.endBlock()
    codeWriter.endBlock()
}

function CheckIsNormalType(type) {

    for (let i = 0; i < MemberTypeList.length; i++) {
        let memberType = MemberTypeList[i];
        if (memberType == type) {
            return true;
        }
    }
    return false;
}
// MemberTypeList.length public : string
// public varName : string
// public type : string
// public index : number
// public group : number
// public res : FairyEditor.FPackageItem
// public constructor ()


function onPublish(handler) {
    let settings = handler.project.GetSettings("Publish").codeGeneration;
    getMemberByName = settings.getMemberByName;
    let codePkgName = handler.ToFilename(handler.pkg.name);
    let writer = new CodeWriter_1.default();
    let classes = handler.CollectClasses(false, false, '')
    let count = classes.Count;
    console.log("classes count ", count)
    let exportCodePath = handler.exportCodePath;
    handler.SetupCodeFolder(exportCodePath, "cs");
    for (let i = 0; i < count; i++) {

        let item = classes.get_Item(i);

        if (item.res != null && !item.res.exported) {

            continue
        }
        // if (item.name == null) {
        //     continue
        // }

        //item.className.indexOf("Layer") > -1 || item.className.indexOf("Cell") > -1 || item.className.indexOf("BagList") > -1
        if (EndWith(item.className, "Layer")) {

            PublishViewCode(handler, writer, item, exportCodePath);
            PublishLogicCode(handler, writer, item, exportCodePath);
            PublishHotfixCode(handler, writer, item, exportCodePath);
            PublishHotfixEventCode(handler, writer, item, exportCodePath);
        }
    }

    PublishFGUIComponentId(handler, writer, classes, exportCodePath);

    PublishItemResourcesMap(handler, writer, exportCodePath);


}
exports.onPublish = onPublish;
function onDestroy() {
    //do cleanup here
}

function EndWith(name, str) {
    return name.match(".*(?:Layer|Cell|BagList)$", str);
}




exports.onDestroy = onDestroy;
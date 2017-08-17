import QtQuick 2.7
import QtQuick.Controls 2.0
import QtQuick.Layouts 1.0
import test 1.1
import test.io 1.0

ApplicationWindow {
    visible: true
    width: 640
    height: 480
    title: qsTr("Hello World")

    SwipeView {
        id: swipeView
        anchors.fill: parent
        currentIndex: tabBar.currentIndex

        Page1 {
        }

        Page {
            Label {
                text: qsTr("Second page")
                anchors.centerIn: parent
            }
        }
    }

	Item {
		Timer {
			interval: 500; running: true; repeat: true
			onTriggered: {
				console.log("testtt")
				var testo = testt.TestObject()
				console.log(testo);
				testo.Test()
				//testo.destroy()
			}
		}

		Text { id: time }
	}

	TestQmlImport {
		id: testt
	}

    footer: TabBar {
        id: tabBar
        currentIndex: swipeView.currentIndex
        TabButton {
            text: qsTr("First")
        }
        TabButton {
            text: qsTr("Second")
        }
    }
}
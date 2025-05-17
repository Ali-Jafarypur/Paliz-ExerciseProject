window.triggerFileDownload = (fileName, url) => {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
}

//window.downloadFileFromStream = async (fileName, contentStreamReference) => {
//    const arrayBuffer = await contentStreamReference.arrayBuffer();
//    const blob = new Blob([arrayBuffer]);
//    const url = URL.createObjectURL(blob);
//    const anchorElement = document.createElement('a');
//    anchorElement.href = url;
//    anchorElement.download = fileName ?? '';
//    anchorElement.click();
//    anchorElement.remove();
//    URL.revokeObjectURL(url);
//}

export function addHandlers() {
    console.log("Handlers added --------------------------- Hello JS");
    // Your code here
}

//export function alertUser() {
//    alert('The button was selected!');

//    //triggerFileDownload(Home.downloadFileName, Home.downloadURL)
//    }
//}

//export function addHandlers() {
//    const btn = document.getElementById("downloadBTN");
//    btn.addEventListener("click", alertUser);
//}
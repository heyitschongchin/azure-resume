window.addEventListener('DOMContentLoaded', (event) =>{
    getVisitCount();
})

const functionApiUrl = 'https://azureresumevisitors.azurewebsites.net/api/azureResumeVisitors?code=rlqn6olC2-qtFL9K2u-Em5H6maSeWaNkVAqafRzwbeDWAzFulEhUwg%3D%3D';
const localfunctionApi = 'http://localhost:7071/api/azureResumeVisitors';

const getVisitCount = () => {
    let count = 30;
    fetch(functionApiUrl).then(response => {
        return response.json()
    }).then(response =>{
        console.log("Website called function API.");
        count = response.count;
        document.getElementById("counter").innerText = count;
    }).catch(function(error){
        console.log(error);
    });
    return count;
}
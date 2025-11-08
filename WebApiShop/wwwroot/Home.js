const postResponse = async () => { 

    const userN = document.querySelector("#userName");
    const fName = document.querySelector("#firstName");
    const lName = document.querySelector("#lastName");
    const pass = document.querySelector("#password");

    const postData = {
    userName: userN.value,
    fName: fName.value,
    lName: lName.value,
    passWord: pass.value,
    id:0
};

    const responsePost = await fetch("https://localhost:44320/api/Users", {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json'
    },
    body: JSON.stringify(postData)
});

const dataPost = await responsePost;
    alert("משתמש נוסף בהצלחה");
}

const login = async () => {
    const userN = document.querySelector("#userN").value;
    const pass = document.querySelector("#pass");
    const postDataLogin = {

        userName: userN,
        fName: "",
        lName: "",
        passWord: pass.value,
        id: 0
    };

    const responsePostLogin = await fetch("https://localhost:44320/api/Users/Login", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(postDataLogin)

    });

    const dataPostLogin = await responsePostLogin.json();
    if (dataPostLogin.status == 204)
        alert("אופסססססססססס המשתמש לא קיים")
    else {
        sessionStorage.setItem("user", JSON.stringify(dataPostLogin))
        alert("wellcome!!!!!!!")
        window.location.href = "../Update.html"
        
    }
      



}
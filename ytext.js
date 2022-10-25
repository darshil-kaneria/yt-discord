function sleep(ms) {
  return new Promise(resolve => setTimeout(resolve, ms));
}

async function getData() {
  while (true) {
    let NameElement = document.getElementsByClassName('title style-scope ytmusic-player-bar')
    let ArtistElement = document.getElementsByClassName('byline style-scope ytmusic-player-bar complex-string')
    let ArtistImageElement = document.getElementsByClassName('image style-scope ytmusic-player-bar')

    if (NameElement[0] == null || ArtistElement[0] == null) {
      console.log("No music playing...")
      await sleep(5 * 1000);
      continue;
    }

    let Name = NameElement[0].textContent
    let Artist = ArtistElement[0].textContent
    let ArtistImage = ArtistImageElement[0].src
    let requrl = "http://localhost:6361/?name=" + encodeURIComponent(Name) + "&artist=" + encodeURIComponent(Artist.split('•')[0]) + "&imgsrc=" + encodeURIComponent(ArtistImage)
    fetch(requrl).then(function (response) {

    }).then(function (data) {

    }).catch(function (err) {
      console.log(err);
    });
    console.log(requrl)
    console.log(Name + ' • ' + Artist.split('•')[0])
    await sleep(4 * 1000);

  }
}

getData();


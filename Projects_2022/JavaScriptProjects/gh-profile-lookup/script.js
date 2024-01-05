$(document).ready(function() {
    $("#searchuser").on("keyup", function(e) {
        let username = e.target.value;

        $.ajax({
            url: `https://api.github.com/users/${username}`,
            data:{
                client_id:'97817cfae14c918a7627',
                client_secret:'f88366bf476335eb9a9dd0e4343477464693f7cf'
            }
        }).done(function(user){
            $.ajax({
                url: `https://api.github.com/users/${username}/repos`,
                data:{
                    client_id:'97817cfae14c918a7627',
                    client_secret:'f88366bf476335eb9a9dd0e4343477464693f7cf',
                    sort:'created: asc',
                    per_page: 5
                }
            }).done(function(repos) {
                $.each(repos, function(index, repo){
                    $("#repos").append(`
                            <div class="row p-3 mb-2 bg-secondary text-dark">
                                <div class="col-md-7">
                                    <strong>${repo.name}</strong>: ${repo.description}
                                </div>
                                <div class="col-md-3">                                
                                    <span class="badge rounded-pill bg-success">Forks: ${repo.forks_count}</span>
                                    <span class="badge rounded-pill bg-info text-dark">Watchers: ${repo.watchers_count}</span>
                                    <span class="badge rounded-pill bg-primary">Stars: ${repo.stargazers_count}</span>
                                </div>
                                <div class="col-md-2">
                                    <a href="${repo.html_url}" target="_blank" class="btn btn-dark">View Page</a>
                                </div>
                            </div>
                    `);
                });
            });
            $("#profile").html(`
            <div class="card border-primary">
                <div class="card-header">
                    <h3 class="card-title">${user.name}</h3>
                </div>
                <div class="card-body text-secondary">
                    <div class="row">
                        <div class="col-md-3">
                            <img class="thumbnail avatar" src="${user.avatar_url}">
                            <a target="_blank" class="btn btn-primary btn-block" href="${user.html_url}">View Profile</a>
                        </div>
                        <div class="col-md-9">
                            <span class="badge rounded-pill bg-success">Public Repos: ${user.public_repos}</span>
                            <span class="badge rounded-pill bg-info text-dark">Public Gists:${user.public_gists}</span>
                            <span class="badge rounded-pill bg-primary">Followers:${user.followers}</span>
                            <span class="badge rounded-pill bg-dark">Following:${user.following}</span>
                            <br><br>
                            <ul class="list-group">
                            <li class="list-group-item">Github ID: ${user.id}</li>
                            <li class="list-group-item">Bio: ${user.bio}</li>
                            <li class="list-group-item">Company: ${user.company}</li>
                            <li class="list-group-item">Website/Blog: ${user.blog}</li>
                            <li class="list-group-item">Location: ${user.location}</li>
                            </ul>                            
                        </div>
                    </div>
                </div>
            </div>

            <div class="card border-success">
                <h3 class="card-header bg-transparent border-success">Latest Repos</h3>
                <div class="card-body container" id="repos"></div>
            </div>
            `)
        });
    });
});
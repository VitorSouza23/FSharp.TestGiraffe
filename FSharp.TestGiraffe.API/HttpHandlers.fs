module HttpHandlers
    
    open Microsoft.AspNetCore.Http
    open Giraffe
    open Models

    let handlerGetHello =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let response = { Text = "Hello World, from Giraffe" }
                return! json response next ctx
            }

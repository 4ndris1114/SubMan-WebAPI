# SubMan-WebAPI
Subscription Manager tool for a school project for Development Environments course :)

<pre><code>```mermaid graph TD A[Push or PR to 'main'] --> B[Checkout Repository] B --> C[Setup .NET 8.0] C --> D[Cache NuGet Packages] D --> E[Restore Dependencies (./API)] E --> F[Build Project (./API)] F --> G[Test Project (./Subman.Tests)] G --> H{Is branch 'main'?} H -- Yes --> I[Trigger Deploy Hook to Render] H -- No --> J[End] ```</code></pre>

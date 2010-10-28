# XeroConnector

I've put this up to force myself to work on it some more. Feel free to send me any pull requests or feature ideas!

## Introduction

This library wraps the Xero version 2 API <http://api.xero.com/>. 
For detailed information on what you can get out of the Xero API, see
the developer documentation here: <http://blog.xero.com/developer/>

## Usage

The current version of XeroConnector only supports Private applications.

### Creating a Xero Session

<pre><code>
var connectionFactory = XeroConnectionFactory("Your OAuth consumer key", "Your OAuth consumer secret", "Xero v2 API Test User", "private_key.pfx", "private key password");
var session = new XeroSession(connectionFactory.CreateXeroConnection());
</code></pre>

The XeroConnectionFactory can also be initialised from a file (in case you don't want to commit this information
into a public source code repository for instance). The file format is pretty simple - stick each of those
strings on a separate line (without the ""s) in the same order as the constructor above, and use this code instead:

<pre><code>
var connectionFactory = XeroConnectionFactory("configfile.cfg");
var session = new XeroSession(connectionFactory.CreateXeroConnection());
</code></pre>

### Using the Session object

The `IXeroSession` interface defines a bunch of methods for getting information out of Xero. *Note that there 
is no error handling yet. At all.* I haven't decided how to do this yet, but there are two options: 1) Throw an exception when an API call fails, or 2) Return a
Result<TModel> object that has the status code + result information. I'm learning towards the latter, although it would require a `if(request.Success) { /* do work */ }`
type usage pattern that I am not fond of. 

See the [IXeroSession](http://github.com/jamiepenney/XeroConnector/blob/master/XeroConnector/Interfaces/IXeroSession.cs) interface for more information on what calls are available.

Each of the IXeroSession methods returns either a Model object or a collection of Model objects. In general the Xero API will return full information for a query when you request a single object, but only partial information when requesting a list of objects, so some properties will be lazy loaded if you access them. If you look at the interface for the object you are requesting, you will see some of them have properties marked as \[LazyLoad\]. Accessing these properties from an object loaded via a multi-object request will result in another call to the Xero API to get the full object. 

For example, say we have an Organisation with 3 invoices dated after the 1st of January 2010. If we run the following code we will end up with 4 calls to Xero:

<pre><code>
var invoices = session.GetInvoices(modifiedAfter: new DateTime(2010, 01, 01))
foreach(var invoice in invoices)
{
	Console.WriteLine(invoice.InvoiceLines.Count);
}
</code></pre>

This will result in the following calls:

* `/api.xro/2.0/invoices`
* `/api.xro/2.0/invoice`
* `/api.xro/2.0/invoice`
* `/api.xro/2.0/invoice`

However, once a Model object has been fully populated (whether by getting the full version in the first place, or by accessing a LazyLoad property), it will never need to go back to the server again. So in the example above, you could have a second for loop accessing the Payments collection, and it wouldn't require any more API requests.
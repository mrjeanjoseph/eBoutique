USE[JSON-XML-TDB]
BEGIN
    --
    SELECT TOP 10
        Post_xml
    FROM Posts;
END

BEGIN
    --Selecting Data from XML: OPENXML and the query and the value Methods
    DECLARE @Dochandle INT;
    DECLARE @xmlDocument NVARCHAR(MAX);

    SET @xmlDocument = N'
	<ROOT>
		<Users Id="227" DisplayName="Amir Ali Akbari" Reputation="805">
			<Posts Title="Publicly Available Datasets" Tags="&lt;open-source&gt;&lt;dataset&gt;">
				<Comments Text="Cross-link: [A database of open databases?](http://opendata.stackexchange.com/questions/266/a-database-of-open-databases)"/>
				<Comments Text="This question might be more appropriate on the dedicated [opendata.SE](http://opendata.stackexchange.com/). That said, I cross my fingers for [dat](http://usodi.org/2014/04/02/dat), which aspires to become a &quot;Git for data&quot;."/>
				<Comments Text="@ojdo Thanks, I never heard of opendata.SE before, I also found [this](http://opendata.stackexchange.com/q/266/2872) interesting (and very similar) question there."/>
				<Comments Text="See http://www.quora.com/Where-can-I-find-large-datasets-open-to-the-public."/>
			</Posts>
		</Users>
		<Users Id="8820" DisplayName="Martin Thoma" Reputation="6748">
			<Posts Title="What are deconvolutional layers?" Tags="&lt;neural-network&gt;&lt;convnet&gt;&lt;convolution&gt;">
				<Comments Text="Hoping it could be useful to anyone, I made a [notebook](https://gist.github.com/akiross/754c7b87a2af8603da78b46cdaaa5598) to explore how convolution and transposed convolution can be used in TensorFlow (0.11). Maybe having some practical examples and figures may help a bit more to understand how they works."/>
			</Posts>
		</Users>
		<Users Id="227" DisplayName="Amir Ali Akbari" Reputation="805">
			<Posts Title="Publicly Available Datasets" Tags="&lt;open-source&gt;&lt;dataset&gt;">
				<Comments Text="https://zenodo.org/"/>
				<Comments Text="Reserve Bank of India have a huge database about India,\nWorld Bank have huge data set"/>
				<Comments Text="I havent found any good free comprehensive datasets for typical Business Intelligence applications.  The [Microsoft Contoso BI Demo Dataset for Retail Industry from Official Microsoft Download Center](http://www.microsoft.com/en-us/download/details.aspx?id=18279) download works with some Microsoft products (see [AndyGett on SharePoint and Other Business Software](http://bl
		og.bullseyeconsulting.com/archive/2012/08/14/setting-up-sample-contoso-database-for-performancepoint-and-sharepoint.aspx)), but I dont see any plain sql or csv dumps of it, nor any license info."/>
				<Comments Text="A great place to find public data sets is http://opendata.stackexchange.com/"/>
				<Comments Text="Have you joined the Open Data Stack Exchange? http://opendata.stackexchange.com"/>
			</Posts>
		</Users>
		<Users Id="8820" DisplayName="Martin Thoma" Reputation="6748">
			<Posts Title="What are deconvolutional layers?" Tags="&lt;neural-network&gt;&lt;convnet&gt;&lt;convolution&gt;">
				<Comments Text="This video lecture explains deconvolution/upsampling:\nhttps://youtu.be/ByjaPdWXKJ4?t=16m59s"/>
				<Comments Text="For me, this page gave me a better explanation it also explains the difference between deconvolution and transpose convolution: https://towardsdatascience.com/types-of-convolutions-in-deep-learning-717013397f4d"/>
				<Comments Text="Isnt upsampling more like backwards pooling than backwards strided convolution, since it has no parameters?"/>
			</Posts>
		</Users>
	</ROOT>';

    EXEC sp_xml_preparedocument @docHandle OUTPUT, @xmlDocument;

    SELECT *
    FROM OPENXML(@docHandle, N'ROOT/Users/Posts/Comments')
	WITH (
		Author NVARCHAR(255) '../../@DisplayName',
		Title NVARCHAR(255) '../@Title',
		Tags NVARCHAR(255) '../@Tags',
		Comment NVARCHAR(255) '@Text'
	);

    EXEC sp_xml_removedocument @docHandle;

    --
    SELECT
        Post_xml.value('(/root/Posts/@Id)[1]', 'INT') AS [Post Id],
        Post_xml.value('(/root/Posts/@Title)[1]', 'NVARCHAR(255)') AS [Post Title],
        Post_xml.value('(/root/Posts/@CreationDate)[1]','DATETIME') AS [Post Creation],
        Post_xml.query('(/root/Posts/Comments)') AS [Post Author]
    FROM Posts
    WHERE
		Post_xml.value('(/root/Posts/@Score)[1]','INT') > 100
        AND
        Post_xml.value('(/root/Posts/@Title)[1]','NVARCHAR(512)') IS NOT NULL;

    --Getting to know the XPath Syntax and XQuery Language
    SELECT TOP 10
        Id,
        Post_xml.value('(//Posts/@Title)[1]', 'NVARCHAR(255)') AS [Post Title],
        Post_xml.value('(//Author/@DisplayName)[1]', 'NVARCHAR(64)') AS [Author Name],
        Post_xml.query('//Comment[@Score > 2]') AS [Relevant Comments]
    FROM Posts
    WHERE
		Post_xml.value('(//Posts/@Title)[1]', 'NVARCHAR(64)') IS NOT NULL
    ORDER BY Post_xml.value('(//Posts/@Score)[1]', 'INT') DESC;

    --Providing Rowset views with OPENXML and the nodes Method
    DECLARE @DocHandle3 INT
    DECLARE @XmlDocument3 NVARCHAR(1000)

    SET @xmlDocument3 = N'
	<root>
	  <Post>
		<Id>22</Id>
		<Title>K-Means clustering for mixed numeric and categorical data</Title>
		<Score>129</Score>
	  </Post>
	  <Post>
		<Id>155</Id>
		<Title>Publicly Available Datasets</Title>
		<Score>164</Score>
	  </Post>
	  <Post>
		<Id>694</Id>
		<Title>Best python library for neural networks</Title>
		<Score>131</Score>
	  </Post>
	  <Post>
		<Id>5706</Id>
		<Title>What is the "dying ReLU" problem in neural networks?</Title>
		<Score>104</Score>
	  </Post>
	  <Post>
		<Id>6107</Id>
		<Title>What are deconvolutional layers?</Title>
		<Score>176</Score>
	  </Post>
	  <Post>
		<Id>9302</Id>
		<Title>The cross-entropy error function in neural networks</Title>
		<Score>104</Score>
	  </Post>
	  <Post>
		<Id>13490</Id>
		<Title>How to set class weights for imbalanced classes in Keras?</Title>
		<Score>109</Score>
	  </Post>
	</root>'

    EXEC sp_xml_preparedocument @DocHandle3 OUTPUT, @XmlDocument3

    SELECT *
    FROM OPENXML (@DocHandle3, '/root/Post',2)
	WITH (
		Id INT,
		Title NVARCHAR(128),
		Score INT )
    EXEC sp_xml_removedocument @DocHandle3


    -- With a node function
    DECLARE @xml xml
    SET @xml = N'
	<root>
	  <Post>
		<Id>22</Id>
		<Title>K-Means clustering for mixed numeric and categorical data</Title>
		<Score>129</Score>
	  </Post>
	  <Post>
		<Id>155</Id>
		<Title>Publicly Available Datasets</Title>
		<Score>164</Score>
	  </Post>
	  <Post>
		<Id>694</Id>
		<Title>Best python library for neural networks</Title>
		<Score>131</Score>
	  </Post>
	  <Post>
		<Id>5706</Id>
		<Title>What is the "dying ReLU" problem in neural networks?</Title>
		<Score>104</Score>
	  </Post>
	  <Post>
		<Id>6107</Id>
		<Title>What are deconvolutional layers?</Title>
		<Score>176</Score>
	  </Post>
	  <Post>
		<Id>9302</Id>
		<Title>The cross-entropy error function in neural networks</Title>
		<Score>104</Score>
	  </Post>
	  <Post>
		<Id>13490</Id>
		<Title>How to set class weights for imbalanced classes in Keras?</Title>
		<Score>109</Score>
	  </Post>
	</root>'

    SELECT
        doc.col.value('Id[1]','INT') [Id],
        doc.col.value('Title[1]', 'NVARCHAR(128)') [Title],
        doc.col.value('Score[1]','INT') [Score]
    FROM @xml.nodes('/root/Post') doc(col)
END

BEGIN
    -- Updating XML with modify and the XML Data Modification Language
    DECLARE @xmlDocument4 xml;

    SET @xmlDocument4 = N'
	<root>
	  <Post>
		<Id>155</Id>
		<Title>Publicly Available Datasets</Title>
		<Score>164</Score>
	  </Post>
	  <Post>
		<Id>694</Id>
		<Title>Best python library for Machine Learning</Title>
		<Score>131</Score>
	  </Post>
	  <Post>
		<Id>6107</Id>
		<Title>What are deconvolutional layers?</Title>
		<Score>176</Score>
	  </Post>
	</root>
	';
    SELECT @xmlDocument4;

    SET @xmlDocument4.modify('
	replace value of (/root/Post/Title/text())[2]
	with "JEAN IS THE BEST OF BUDDIES"');

    SELECT @xmlDocument4;
END

BEGIN
    --Filtering XML Data with Contains and Exist
    SELECT
        Id,
        Post_xml.value('(//Posts/@Title)[1]', 'NVARCHAR(256)') AS [Post Title],
        Post_xml.value('(//Posts/@Tags)[1]', 'NVARCHAR(256)') AS [Post Tags],
        Post_xml.query('//Comment') AS [All Comments]
    FROM Posts
    WHERE Post_xml.exist('//Comment') = 1
        AND Post_xml.exist('(//Posts[contains(@Tags, "machine-learning")])[1]') = 1;
END
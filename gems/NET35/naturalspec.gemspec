version = File.read(File.expand_path("../VERSION",__FILE__)).strip

Gem::Specification.new do |spec|
  spec.platform    = Gem::Platform::RUBY
  spec.name        = 'naturalspec'
  spec.version     = version
  spec.files       = Dir['lib/**/*'] + Dir['docs/**/*']
  spec.add_dependency('nunit')
  
  spec.summary     = 'NaturalSpec is a .NET UnitTest framework which provides automatically testable specs in natural language.'
  spec.description = "NaturalSpec is a .NET UnitTest framework which provides automatically testable specs in natural language. NaturalSpec is based on NUnit and completely written in F# - but you don't have to learn F# to use it."      
  
  spec.authors           = 'Steffen Forkmann'
  spec.email             = 'forkmann@gmx.de'
  spec.homepage          = 'http://github.com/forki/NaturalSpec'
  spec.rubyforge_project = 'naturalspec'
end